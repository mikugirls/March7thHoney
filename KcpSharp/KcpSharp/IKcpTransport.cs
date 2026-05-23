using System.Net;

namespace March7thHoney.Kcp.KcpSharp;




public interface IKcpTransport
{
    
    
    
    
    
    
    
    ValueTask SendPacketAsync(Memory<byte> packet, IPEndPoint remoteEndpoint, CancellationToken cancellationToken);
}