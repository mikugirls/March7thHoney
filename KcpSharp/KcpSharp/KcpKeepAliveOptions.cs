namespace March7thHoney.Kcp.KcpSharp;




public sealed class KcpKeepAliveOptions
{
    
    
    
    
    
    
    
    
    public KcpKeepAliveOptions(int sendInterval, int gracePeriod)
    {
        if (sendInterval <= 0) throw new ArgumentOutOfRangeException(nameof(sendInterval));
        if (gracePeriod <= 0) throw new ArgumentOutOfRangeException(nameof(gracePeriod));
        SendInterval = sendInterval;
        GracePeriod = gracePeriod;
    }

    internal int SendInterval { get; }
    internal int GracePeriod { get; }
}