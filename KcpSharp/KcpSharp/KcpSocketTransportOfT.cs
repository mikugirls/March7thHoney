using System.Buffers;
using System.Net;
using System.Net.Sockets;
using March7thHoney.Util;

namespace March7thHoney.Kcp.KcpSharp;





public abstract class KcpSocketTransport<T> : IKcpTransport, IDisposable where T : class, IKcpConversation
{
    private readonly int _mtu;
    private readonly UdpClient _udpListener;
    private T? _connection;
    private CancellationTokenSource? _cts;
    private bool _disposed;

    
    
    
    
    
    protected KcpSocketTransport(UdpClient listener, int mtu)
    {
        _udpListener = listener ?? throw new ArgumentNullException(nameof(listener));
        _mtu = mtu;
        if (mtu < 50) throw new ArgumentOutOfRangeException(nameof(mtu));
    }

    
    
    
    
    
    public T Connection => _connection ?? throw new InvalidOperationException();

    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    
    public ValueTask SendPacketAsync(Memory<byte> packet, IPEndPoint endpoint,
        CancellationToken cancellationToken = default)
    {
        if (_disposed) return default;
        if (packet.Length > _mtu) return default;

        return new ValueTask(_udpListener.SendAsync(packet.ToArray(), endpoint, cancellationToken).AsTask());
    }

    
    
    
    
    protected abstract T Activate();

    
    
    
    
    
    protected virtual IMemoryOwner<byte> AllocateBuffer(int size)
    {
#if NEED_POH_SHIM
            return MemoryPool<byte>.Shared.Rent(size);
#else
        return new ArrayMemoryOwner(GC.AllocateUninitializedArray<byte>(size, true));
#endif
    }

    
    
    
    
    
    protected virtual bool HandleException(Exception ex)
    {
        return false;
    }

    
    
    
    public void Start()
    {
        if (_disposed) throw new ObjectDisposedException(nameof(KcpSocketTransport));
        if (_connection is not null) throw new InvalidOperationException();

        _connection = Activate();
        if (_connection is null) throw new InvalidOperationException();
        _cts = new CancellationTokenSource();
        RunReceiveLoop();
    }

    private async void RunReceiveLoop()
    {
        var cancellationToken = _cts?.Token ?? new CancellationToken(true);
        IKcpConversation? connection = _connection;
        if (connection is null || cancellationToken.IsCancellationRequested) return;

        using var memoryOwner = AllocateBuffer(_mtu);
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var bytesReceived = 0;
                var error = false;
                UdpReceiveResult result = default;
                try
                {
                    result = await _udpListener.ReceiveAsync(cancellationToken);
                    bytesReceived = result.Buffer.Length;
                }
                catch
                {
                }

                if (bytesReceived != 0 && bytesReceived <= _mtu)
                {
                    if (bytesReceived == March7thHoneyConnection.HANDSHAKE_SIZE)
                        await March7thHoneyListener.HandleHandshake(result);
                    else if (!error)
                        await connection.InputPakcetAsync(result, cancellationToken).ConfigureAwait(false);
                }
            }
        }
        catch (OperationCanceledException)
        {
            
        }
        catch (Exception ex)
        {
            HandleExceptionWrapper(ex);
        }
    }

    private bool HandleExceptionWrapper(Exception ex)
    {
        bool result;
        try
        {
            new Logger("KcpServer").Error("KCP Error:", ex);
            result = HandleException(ex);
        }
        catch
        {
            result = false;
        }

        _connection?.SetTransportClosed();
        var cts = Interlocked.Exchange(ref _cts, null);
        if (cts is not null)
        {
            cts.Cancel();
            cts.Dispose();
        }

        return result;
    }

    
    
    
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                var cts = Interlocked.Exchange(ref _cts, null);
                if (cts is not null)
                {
                    cts.Cancel();
                    cts.Dispose();
                }

                _connection?.Dispose();
            }

            _connection = null;
            _cts = null;
            _disposed = true;
        }
    }

    
    
    
    ~KcpSocketTransport()
    {
        Dispose(false);
    }
}