namespace March7thHoney.Kcp.KcpSharp;




public class KcpConversationOptions
{
    internal const int MtuDefaultValue = 1400;
    internal const uint SendWindowDefaultValue = 32;
    internal const uint ReceiveWindowDefaultValue = 128;
    internal const uint RemoteReceiveWindowDefaultValue = 128;
    internal const uint UpdateIntervalDefaultValue = 100;

    internal const int SendQueueSizeDefaultValue = 32;
    internal const int ReceiveQueueSizeDefaultValue = 32;

    
    
    
    public IKcpBufferPool? BufferPool { get; set; }

    
    
    
    public int Mtu { get; set; } = 1400;

    
    
    
    public int SendWindow { get; set; } = 32;

    
    
    
    public int ReceiveWindow { get; set; } = 128;

    
    
    
    public int RemoteReceiveWindow { get; set; } = 128;

    
    
    
    public int UpdateInterval { get; set; } = 100;

    
    
    
    public bool NoDelay { get; set; }

    
    
    
    public int FastResend { get; set; }

    
    
    
    public bool DisableCongestionControl { get; set; }

    
    
    
    public bool StreamMode { get; set; }

    
    
    
    public int SendQueueSize { get; set; }

    
    
    
    public int ReceiveQueueSize { get; set; }

    
    
    
    
    public int PreBufferSize { get; set; }

    
    
    
    
    public int PostBufferSize { get; set; }

    
    
    
    public KcpKeepAliveOptions? KeepAliveOptions { get; set; }

    
    
    
    public KcpReceiveWindowNotificationOptions? ReceiveWindowNotificationOptions { get; set; }
}