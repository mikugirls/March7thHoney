using System.Net;

namespace March7thHoney.Kcp.KcpSharp;




public interface IKcpMultiplexConnection : IDisposable
{
    
    
    
    
    
    bool Contains(long id);

    
    
    
    
    
    
    
    
    
    KcpRawChannel CreateRawChannel(long id, IPEndPoint remoteEndpoint, KcpRawChannelOptions? options = null);

    
    
    
    
    
    
    
    
    
    KcpConversation CreateConversation(long id, IPEndPoint remoteEndpoint, KcpConversationOptions? options = null);

    
    
    
    
    
    
    
    
    void RegisterConversation(IKcpConversation conversation, long id);


    
    
    
    
    
    IKcpConversation? UnregisterConversation(long id);
}