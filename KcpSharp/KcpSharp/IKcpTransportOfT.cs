namespace March7thHoney.Kcp.KcpSharp;





public interface IKcpTransport<out T> : IKcpTransport, IKcpExceptionProducer<IKcpTransport<T>>, IDisposable
{
    
    
    
    
    
    T Connection { get; }

    
    
    
    
    
    void Start();
}