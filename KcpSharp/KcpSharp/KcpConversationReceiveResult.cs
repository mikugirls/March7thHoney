using System.Globalization;

namespace March7thHoney.Kcp.KcpSharp;




public readonly struct KcpConversationReceiveResult : IEquatable<KcpConversationReceiveResult>
{
    private readonly bool _connectionAlive;

    
    
    
    public int BytesReceived { get; }

    
    
    
    public bool TransportClosed => !_connectionAlive;

    
    
    
    
    public KcpConversationReceiveResult(int bytesReceived)
    {
        BytesReceived = bytesReceived;
        _connectionAlive = true;
    }

    
    
    
    
    
    
    public static bool operator ==(KcpConversationReceiveResult left, KcpConversationReceiveResult right)
    {
        return left.Equals(right);
    }

    
    
    
    
    
    
    public static bool operator !=(KcpConversationReceiveResult left, KcpConversationReceiveResult right)
    {
        return !left.Equals(right);
    }

    
    public bool Equals(KcpConversationReceiveResult other)
    {
        return BytesReceived == other.BytesReceived && TransportClosed == other.TransportClosed;
    }

    
    public override bool Equals(object? obj)
    {
        return obj is KcpConversationReceiveResult other && Equals(other);
    }

    
    public override int GetHashCode()
    {
        return HashCode.Combine(BytesReceived, TransportClosed);
    }

    
    public override string ToString()
    {
        return _connectionAlive ? BytesReceived.ToString(CultureInfo.InvariantCulture) : "Transport is closed.";
    }
}