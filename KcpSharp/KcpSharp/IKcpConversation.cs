using System.Net.Sockets;

namespace March7thHoney.Kcp.KcpSharp;




public interface IKcpConversation : IDisposable
{
    
    
    
    
    
    
    
    
    
    ValueTask InputPakcetAsync(UdpReceiveResult packet, CancellationToken cancellationToken);

    
    
    
    void SetTransportClosed();
}