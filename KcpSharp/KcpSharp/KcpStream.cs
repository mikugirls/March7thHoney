namespace March7thHoney.Kcp.KcpSharp;




public sealed class KcpStream : Stream
{
    private readonly bool _ownsConversation;
    private KcpConversation? _conversation;

    
    
    
    
    
    
    
    
    public KcpStream(KcpConversation conversation, bool ownsConversation)
    {
        if (conversation is null) throw new ArgumentNullException(nameof(conversation));
        if (!conversation.StreamMode)
            throw new ArgumentException("Non-stream mode conversation is not supported.", nameof(conversation));
        _conversation = conversation;
        _ownsConversation = ownsConversation;
    }

    
    public override bool CanRead => true;

    
    public override bool CanSeek => false;

    
    public override bool CanWrite => true;

    
    
    
    public override long Length => throw new NotSupportedException();

    
    
    
    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    
    
    
    
    public bool DataAvailable
    {
        get
        {
            if (_conversation is null) ThrowHelper.ThrowObjectDisposedForKcpStreamException();
            return _conversation!.TryPeek(out var result) && result.BytesReceived != 0;
        }
    }

    
    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    
    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }

    
    public override void Flush()
    {
        throw new NotSupportedException();
    }

    
    public override Task FlushAsync(CancellationToken cancellationToken)
    {
        if (_conversation is null) return Task.FromException(ThrowHelper.NewObjectDisposedForKcpStreamException());
        return _conversation!.FlushAsync(cancellationToken).AsTask();
    }

    
    public override int Read(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException();
    }

    
    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException();
    }

    
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        if (_conversation is null) return Task.FromException<int>(new ObjectDisposedException(nameof(KcpStream)));
        return _conversation.ReadAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();
    }

    
    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        if (_conversation is null) return Task.FromException(new ObjectDisposedException(nameof(KcpStream)));
        return _conversation.WriteAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();
    }

    
    public override int ReadByte()
    {
        throw new NotSupportedException();
    }

    
    public override void WriteByte(byte value)
    {
        throw new NotSupportedException();
    }

    
    protected override void Dispose(bool disposing)
    {
        if (disposing && _ownsConversation) _conversation?.Dispose();
        _conversation = null;
        base.Dispose(disposing);
    }

#if !NO_FAST_SPAN
    
    public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        if (_conversation is null)
            return new ValueTask<int>(Task.FromException<int>(new ObjectDisposedException(nameof(KcpStream))));
        return _conversation.ReadAsync(buffer, cancellationToken);
    }

    
    public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        if (_conversation is null)
            return new ValueTask(Task.FromException(new ObjectDisposedException(nameof(KcpStream))));
        return _conversation.WriteAsync(buffer, cancellationToken);
    }

    
    public override ValueTask DisposeAsync()
    {
        if (_conversation is not null)
        {
            _conversation.Dispose();
            _conversation = null;
        }

        return base.DisposeAsync();
    }

    
    public override int Read(Span<byte> buffer)
    {
        throw new NotSupportedException();
    }

    
    public override void Write(ReadOnlySpan<byte> buffer)
    {
        throw new NotSupportedException();
    }
#endif
}