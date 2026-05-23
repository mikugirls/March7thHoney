using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace March7thHoney.Kcp.KcpSharp;





public sealed class KcpMultiplexConnection<T> : IKcpTransport, IKcpConversation, IKcpMultiplexConnection<T>
{
    private readonly ConcurrentDictionary<long, (IKcpConversation Conversation, T? State)> _conversations = new();

    private readonly Action<T?>? _disposeAction;
    private readonly IKcpTransport _transport;
    private bool _disposed;
    private bool _transportClosed;

    
    
    
    
    public KcpMultiplexConnection(IKcpTransport transport)
    {
        _transport = transport ?? throw new ArgumentNullException(nameof(transport));
        _disposeAction = null;
    }

    
    
    
    
    
    public KcpMultiplexConnection(IKcpTransport transport, Action<T?>? disposeAction)
    {
        _transport = transport ?? throw new ArgumentNullException(nameof(transport));
        _disposeAction = disposeAction;
    }

    
    
    
    
    
    
    
    
    
    public ValueTask InputPakcetAsync(UdpReceiveResult packet, CancellationToken cancellationToken = default)
    {
        ReadOnlySpan<byte> span = packet.Buffer.AsSpan();
        if (span.Length < KcpGlobalVars.CONVID_LENGTH) return default;
        if (_transportClosed || _disposed) return default;
        var id = BinaryPrimitives.ReadInt64BigEndian(span);
        if (_conversations.TryGetValue(id, out var value))
            return value.Conversation.InputPakcetAsync(packet, cancellationToken);
        return default;
    }

    
    public void SetTransportClosed()
    {
        _transportClosed = true;
        foreach (var (conversation, _) in _conversations.Values) conversation.SetTransportClosed();
    }

    
    public void Dispose()
    {
        if (_disposed) return;
        _transportClosed = true;
        _disposed = true;
        while (!_conversations.IsEmpty)
            foreach (var id in _conversations.Keys)
                if (_conversations.TryRemove(id, out var value))
                {
                    value.Conversation.Dispose();
                    if (_disposeAction is not null) _disposeAction.Invoke(value.State);
                }
    }

    
    
    
    
    
    public bool Contains(long id)
    {
        CheckDispose();
        return _conversations.ContainsKey(id);
    }

    
    
    
    
    
    
    
    
    
    public KcpRawChannel CreateRawChannel(long id, IPEndPoint remoteEndpoint, KcpRawChannelOptions? options = null)
    {
        KcpRawChannel? channel = new(remoteEndpoint, this, id, options);
        try
        {
            RegisterConversation(channel, id, default);
            if (_transportClosed) channel.SetTransportClosed();
            return Interlocked.Exchange(ref channel, null)!;
        }
        finally
        {
            if (channel is not null) channel.Dispose();
        }
    }

    
    
    
    
    
    
    
    
    
    
    public KcpRawChannel CreateRawChannel(long id, IPEndPoint remoteEndpoint, T state,
        KcpRawChannelOptions? options = null)
    {
        KcpRawChannel? channel = new(remoteEndpoint, this, id, options);
        try
        {
            RegisterConversation(channel, id, state);
            if (_transportClosed) channel.SetTransportClosed();
            return Interlocked.Exchange(ref channel, null)!;
        }
        finally
        {
            if (channel is not null) channel.Dispose();
        }
    }

    
    
    
    
    
    
    
    
    
    public KcpConversation CreateConversation(long id, IPEndPoint remoteEndpoint,
        KcpConversationOptions? options = null)
    {
        KcpConversation? conversation = new(remoteEndpoint, this, id, options);
        try
        {
            RegisterConversation(conversation, id, default);
            if (_transportClosed) conversation.SetTransportClosed();
            return Interlocked.Exchange(ref conversation, null)!;
        }
        finally
        {
            if (conversation is not null) conversation.Dispose();
        }
    }

    
    
    
    
    
    
    
    
    
    
    public KcpConversation CreateConversation(long id, IPEndPoint remoteEndpoint, T state,
        KcpConversationOptions? options = null)
    {
        KcpConversation? conversation = new(remoteEndpoint, this, id, options);
        try
        {
            RegisterConversation(conversation, id, state);
            if (_transportClosed) conversation.SetTransportClosed();
            return Interlocked.Exchange(ref conversation, null)!;
        }
        finally
        {
            if (conversation is not null) conversation.Dispose();
        }
    }

    
    
    
    
    
    
    
    
    public void RegisterConversation(IKcpConversation conversation, long id)
    {
        RegisterConversation(conversation, id, default);
    }

    
    
    
    
    
    
    
    
    
    public void RegisterConversation(IKcpConversation conversation, long id, T? state)
    {
        if (conversation is null) throw new ArgumentNullException(nameof(conversation));

        CheckDispose();
        var (addedConversation, _) = _conversations.GetOrAdd(id, (conversation, state));
        if (!ReferenceEquals(addedConversation, conversation))
            throw new InvalidOperationException("Duplicated conversation.");
        if (_disposed)
        {
            if (_conversations.TryRemove(id, out var value) && _disposeAction is not null)
                _disposeAction.Invoke(value.State);
            ThrowObjectDisposedException();
        }
    }

    
    
    
    
    
    public IKcpConversation? UnregisterConversation(long id)
    {
        return UnregisterConversation(id, out _);
    }

    
    
    
    
    
    
    public IKcpConversation? UnregisterConversation(long id, out T? state)
    {
        if (!_transportClosed && !_disposed && _conversations.TryRemove(id, out var value))
        {
            value.Conversation.SetTransportClosed();
            state = value.State;
            if (_disposeAction is not null) _disposeAction.Invoke(state);
            return value.Conversation;
        }

        state = default;
        return default;
    }

    
    public ValueTask SendPacketAsync(Memory<byte> packet, IPEndPoint remoteEndpoint,
        CancellationToken cancellationToken = default)
    {
        if (_transportClosed || _disposed) return default;
        return _transport.SendPacketAsync(packet, remoteEndpoint, cancellationToken);
    }

    private void CheckDispose()
    {
        if (_disposed) ThrowObjectDisposedException();
    }

    private static void ThrowObjectDisposedException()
    {
        throw new ObjectDisposedException(nameof(KcpMultiplexConnection<T>));
    }
}