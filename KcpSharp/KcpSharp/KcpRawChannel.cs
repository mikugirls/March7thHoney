using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;

namespace March7thHoney.Kcp.KcpSharp;




public sealed class KcpRawChannel : IKcpConversation, IKcpExceptionProducer<KcpRawChannel>
{
    private readonly IKcpBufferPool _bufferPool;
    private readonly ulong? _id;
    private readonly int _mtu;
    private readonly int _postBufferSize;
    private readonly int _preBufferSize;
    private readonly KcpRawReceiveQueue _receiveQueue;
    private readonly IPEndPoint _remoteEndPoint;
    private readonly AsyncAutoResetEvent<int> _sendNotification;
    private readonly KcpRawSendOperation _sendOperation;
    private readonly IKcpTransport _transport;

    private Func<Exception, KcpRawChannel, object?, bool>? _exceptionHandler;
    private object? _exceptionHandlerState;

    private CancellationTokenSource? _sendLoopCts;

    
    
    
    
    
    
    public KcpRawChannel(IPEndPoint remoteEndPoint, IKcpTransport transport, KcpRawChannelOptions? options)
        : this(remoteEndPoint, transport, null, options)
    {
    }

    
    
    
    
    
    
    
    public KcpRawChannel(IPEndPoint remoteEndPoint, IKcpTransport transport, long conversationId,
        KcpRawChannelOptions? options)
        : this(remoteEndPoint, transport, (ulong)conversationId, options)
    {
    }

    private KcpRawChannel(IPEndPoint remoteEndPoint, IKcpTransport transport, ulong? conversationId,
        KcpRawChannelOptions? options)
    {
        _bufferPool = options?.BufferPool ?? DefaultArrayPoolBufferAllocator.Default;
        _remoteEndPoint = remoteEndPoint;
        _transport = transport;
        _id = conversationId;

        if (options is null)
            _mtu = KcpConversationOptions.MtuDefaultValue;
        else if (options.Mtu < 50)
            throw new ArgumentException("MTU must be at least 50.", nameof(options));
        else
            _mtu = options.Mtu;

        _preBufferSize = options?.PreBufferSize ?? 0;
        _postBufferSize = options?.PostBufferSize ?? 0;
        if (_preBufferSize < 0)
            throw new ArgumentException("PreBufferSize must be a non-negative integer.", nameof(options));
        if (_postBufferSize < 0)
            throw new ArgumentException("PostBufferSize must be a non-negative integer.", nameof(options));
        if ((uint)(_preBufferSize + _postBufferSize) >= (uint)_mtu)
            throw new ArgumentException("The sum of PreBufferSize and PostBufferSize must be less than MTU.",
                nameof(options));
        if (conversationId.HasValue && (uint)(_preBufferSize + _postBufferSize) >= (uint)(_mtu - 4))
            throw new ArgumentException(
                "The sum of PreBufferSize and PostBufferSize is too large. There is not enough space in the packet for the conversation ID.",
                nameof(options));

        var queueSize = options?.ReceiveQueueSize ?? 32;
        if (queueSize < 1) throw new ArgumentException("QueueSize must be a positive integer.", nameof(options));

        _sendLoopCts = new CancellationTokenSource();
        _sendNotification = new AsyncAutoResetEvent<int>();
        _receiveQueue = new KcpRawReceiveQueue(_bufferPool, queueSize);
        _sendOperation = new KcpRawSendOperation(_sendNotification);

        RunSendLoop();
    }

    
    
    
    public long? ConversationId => (long?)_id;

    
    
    
    public bool TransportClosed => _sendLoopCts is null;

    
    public ValueTask InputPakcetAsync(UdpReceiveResult packet, CancellationToken cancellationToken = default)
    {
        ReadOnlySpan<byte> span = packet.Buffer.AsSpan();
        var overhead = _id.HasValue ? KcpGlobalVars.CONVID_LENGTH : 0;
        if (span.Length < overhead || span.Length > _mtu) return default;
        if (_id.HasValue)
        {
            if (BinaryPrimitives.ReadUInt64BigEndian(span) != _id.GetValueOrDefault()) return default;
            span = span.Slice(8);
        }

        _receiveQueue.Enqueue(span);
        return default;
    }


    
    public void SetTransportClosed()
    {
        var cts = Interlocked.Exchange(ref _sendLoopCts, null);
        if (cts is not null)
        {
            cts.Cancel();
            cts.Dispose();
        }

        _receiveQueue.SetTransportClosed();
        _sendOperation.SetTransportClosed();
        _sendNotification.Set(0);
    }

    
    public void Dispose()
    {
        SetTransportClosed();
        _receiveQueue.Dispose();
        _sendOperation.Dispose();
    }

    
    
    
    
    
    
    
    public void SetExceptionHandler(Func<Exception, KcpRawChannel, object?, bool> handler, object? state)
    {
        if (handler is null) throw new ArgumentNullException(nameof(handler));

        _exceptionHandler = handler;
        _exceptionHandlerState = state;
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    public ValueTask<bool> SendAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        return _sendOperation.SendAsync(buffer, cancellationToken);
    }


    
    
    
    
    public bool CancelPendingSend()
    {
        return _sendOperation.CancelPendingOperation(null, default);
    }

    
    
    
    
    
    
    
    
    
    
    
    
    public bool CancelPendingSend(Exception? innerException, CancellationToken cancellationToken)
    {
        return _sendOperation.CancelPendingOperation(innerException, cancellationToken);
    }


    private async void RunSendLoop()
    {
        var cancellationToken = _sendLoopCts?.Token ?? new CancellationToken(true);
        var sendOperation = _sendOperation;
        var ev = _sendNotification;
        var mss = _mtu - _preBufferSize - _postBufferSize;
        if (_id.HasValue) mss -= 8;

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var payloadSize = await ev.WaitAsync().ConfigureAwait(false);
                if (cancellationToken.IsCancellationRequested) break;

                if (payloadSize < 0 || payloadSize > mss)
                {
                    _ = sendOperation.TryConsume(default, out _);
                    continue;
                }

                var overhead = _preBufferSize + _postBufferSize;
                if (_id.HasValue) overhead += 8;
                {
                    using var owner = _bufferPool.Rent(new KcpBufferPoolRentOptions(payloadSize + overhead, true));
                    var memory = owner.Memory;

                    
                    if (_preBufferSize != 0)
                    {
                        memory.Span.Slice(0, _preBufferSize).Clear();
                        memory = memory.Slice(_preBufferSize);
                    }

                    if (_id.HasValue)
                    {
                        BinaryPrimitives.WriteUInt64LittleEndian(memory.Span, _id.GetValueOrDefault());
                        memory = memory.Slice(8);
                    }

                    if (!sendOperation.TryConsume(memory, out var bytesWritten)) continue;
                    payloadSize = Math.Min(payloadSize, bytesWritten);
                    memory = memory.Slice(payloadSize);
                    if (_postBufferSize != 0) memory.Span.Slice(0, _postBufferSize).Clear();

                    
                    try
                    {
                        await _transport.SendPacketAsync(owner.Memory.Slice(0, payloadSize + overhead), _remoteEndPoint,
                            cancellationToken).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        if (!HandleFlushException(ex)) break;
                    }
                }
            }
        }
        catch (OperationCanceledException)
        {
            
        }
        catch (Exception ex)
        {
            HandleFlushException(ex);
        }
    }


    private bool HandleFlushException(Exception ex)
    {
        var handler = _exceptionHandler;
        var state = _exceptionHandlerState;
        var result = false;
        if (handler is not null)
            try
            {
                result = handler.Invoke(ex, this, state);
            }
            catch
            {
                result = false;
            }

        if (!result) SetTransportClosed();
        return result;
    }

    
    
    
    
    
    
    
    
    
    public bool TryPeek(out KcpConversationReceiveResult result)
    {
        return _receiveQueue.TryPeek(out result);
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    public bool TryReceive(Span<byte> buffer, out KcpConversationReceiveResult result)
    {
        return _receiveQueue.TryReceive(buffer, out result);
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    public ValueTask<KcpConversationReceiveResult> WaitToReceiveAsync(CancellationToken cancellationToken)
    {
        return _receiveQueue.WaitToReceiveAsync(cancellationToken);
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    public ValueTask<KcpConversationReceiveResult> ReceiveAsync(Memory<byte> buffer,
        CancellationToken cancellationToken = default)
    {
        return _receiveQueue.ReceiveAsync(buffer, cancellationToken);
    }


    
    
    
    
    public bool CancelPendingReceive()
    {
        return _receiveQueue.CancelPendingOperation(null, default);
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    public bool CancelPendingReceive(Exception? innerException, CancellationToken cancellationToken)
    {
        return _receiveQueue.CancelPendingOperation(innerException, cancellationToken);
    }
}