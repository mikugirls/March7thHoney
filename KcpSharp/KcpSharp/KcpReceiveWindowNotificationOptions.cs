namespace March7thHoney.Kcp.KcpSharp;




public sealed class KcpReceiveWindowNotificationOptions
{
    
    
    
    
    
    public KcpReceiveWindowNotificationOptions(int initialInterval, int maximumInterval)
    {
        if (initialInterval <= 0) throw new ArgumentOutOfRangeException(nameof(initialInterval));
        if (maximumInterval < initialInterval) throw new ArgumentOutOfRangeException(nameof(maximumInterval));
        InitialInterval = initialInterval;
        MaximumInterval = maximumInterval;
    }

    
    
    
    public int InitialInterval { get; }

    
    
    
    public int MaximumInterval { get; }
}