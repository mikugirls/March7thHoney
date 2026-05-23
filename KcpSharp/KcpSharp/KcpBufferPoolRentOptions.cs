namespace March7thHoney.Kcp.KcpSharp;




public readonly struct KcpBufferPoolRentOptions : IEquatable<KcpBufferPoolRentOptions>
{
    
    
    
    public int Size { get; }

    
    
    
    
    public bool IsOutbound { get; }

    
    
    
    
    
    
    
    
    public KcpBufferPoolRentOptions(int size, bool isOutbound)
    {
        Size = size;
        IsOutbound = isOutbound;
    }

    
    public bool Equals(KcpBufferPoolRentOptions other)
    {
        return Size == other.Size && IsOutbound == other.IsOutbound;
    }

    
    public override bool Equals(object? obj)
    {
        return obj is KcpBufferPoolRentOptions other && Equals(other);
    }

    
    public override int GetHashCode()
    {
        return HashCode.Combine(Size, IsOutbound);
    }
}