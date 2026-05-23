using System.Net;
using System.Net.Sockets;

namespace March7thHoney.Kcp.KcpSharp;




public static class KcpSocketTransport
{
    
    
    
    
    
    
    
    
    public static IKcpTransport<KcpConversation> CreateConversation(UdpClient listener, IPEndPoint endPoint,
        long conversationId, KcpConversationOptions? options)
    {
        if (listener is null) throw new ArgumentNullException(nameof(listener));
        if (endPoint is null) throw new ArgumentNullException(nameof(endPoint));

        return new KcpSocketTransportForConversation(listener, endPoint, conversationId, options);
    }

    
    
    
    
    
    
    
    public static IKcpTransport<KcpConversation> CreateConversation(UdpClient listener, IPEndPoint endPoint,
        KcpConversationOptions? options)
    {
        if (listener is null) throw new ArgumentNullException(nameof(listener));
        if (endPoint is null) throw new ArgumentNullException(nameof(endPoint));

        return new KcpSocketTransportForConversation(listener, endPoint, null, options);
    }

    
    
    
    
    
    
    
    
    public static IKcpTransport<KcpRawChannel> CreateRawChannel(UdpClient listener, IPEndPoint endPoint,
        long conversationId, KcpRawChannelOptions? options)
    {
        if (listener is null) throw new ArgumentNullException(nameof(listener));
        if (endPoint is null) throw new ArgumentNullException(nameof(endPoint));

        return new KcpSocketTransportForRawChannel(listener, endPoint, conversationId, options);
    }

    
    
    
    
    
    
    
    public static IKcpTransport<KcpRawChannel> CreateRawChannel(UdpClient listener, IPEndPoint endPoint,
        KcpRawChannelOptions? options)
    {
        if (listener is null) throw new ArgumentNullException(nameof(listener));
        if (endPoint is null) throw new ArgumentNullException(nameof(endPoint));

        return new KcpSocketTransportForRawChannel(listener, endPoint, null, options);
    }

    
    
    
    
    
    
    public static IKcpTransport<IKcpMultiplexConnection> CreateMultiplexConnection(UdpClient listener, int mtu)
    {
        if (listener is null) throw new ArgumentNullException(nameof(listener));

        return new KcpSocketTransportForMultiplexConnection<object>(listener, mtu);
    }

    
    
    
    
    
    
    
    public static IKcpTransport<IKcpMultiplexConnection<T>> CreateMultiplexConnection<T>(UdpClient listener,
        IPEndPoint endPoint, int mtu)
    {
        if (listener is null) throw new ArgumentNullException(nameof(listener));
        if (endPoint is null) throw new ArgumentNullException(nameof(endPoint));

        return new KcpSocketTransportForMultiplexConnection<T>(listener, mtu);
    }

    
    
    
    
    
    
    
    
    
    public static IKcpTransport<IKcpMultiplexConnection<T>> CreateMultiplexConnection<T>(UdpClient listener,
        EndPoint endPoint, int mtu, Action<T?>? disposeAction)
    {
        if (listener is null) throw new ArgumentNullException(nameof(listener));
        if (endPoint is null) throw new ArgumentNullException(nameof(endPoint));

        return new KcpSocketTransportForMultiplexConnection<T>(listener, mtu, disposeAction);
    }
}