namespace March7thHoney.Kcp.KcpSharp;




public static class KcpExceptionProducerExtensions
{
    
    
    
    
    
    
    public static void SetExceptionHandler<T>(this IKcpExceptionProducer<T> producer, Func<Exception, T, bool> handler)
    {
        if (producer is null) throw new ArgumentNullException(nameof(producer));
        if (handler is null) throw new ArgumentNullException(nameof(handler));

        producer.SetExceptionHandler(
            (ex, conv, state) => ((Func<Exception, T, bool>?)state)!.Invoke(ex, conv),
            handler
        );
    }

    
    
    
    
    
    
    public static void SetExceptionHandler<T>(this IKcpExceptionProducer<T> producer, Func<Exception, bool> handler)
    {
        if (producer is null) throw new ArgumentNullException(nameof(producer));
        if (handler is null) throw new ArgumentNullException(nameof(handler));

        producer.SetExceptionHandler(
            (ex, conv, state) => ((Func<Exception, bool>?)state)!.Invoke(ex),
            handler
        );
    }

    
    
    
    
    
    
    public static void SetExceptionHandler<T>(this IKcpExceptionProducer<T> producer,
        Action<Exception, T, object?> handler, object? state)
    {
        if (producer is null) throw new ArgumentNullException(nameof(producer));
        if (handler is null) throw new ArgumentNullException(nameof(handler));

        producer.SetExceptionHandler(
            (ex, conv, state) =>
            {
                var tuple = (Tuple<Action<Exception, T, object?>, object?>)state!;
                tuple.Item1.Invoke(ex, conv, tuple.Item2);
                return false;
            },
            Tuple.Create(handler, state)
        );
    }

    
    
    
    
    
    public static void SetExceptionHandler<T>(this IKcpExceptionProducer<T> producer, Action<Exception, T> handler)
    {
        if (producer is null) throw new ArgumentNullException(nameof(producer));
        if (handler is null) throw new ArgumentNullException(nameof(handler));

        producer.SetExceptionHandler(
            (ex, conv, state) =>
            {
                var handler = (Action<Exception, T>)state!;
                handler.Invoke(ex, conv);
                return false;
            },
            handler
        );
    }

    
    
    
    
    
    public static void SetExceptionHandler<T>(this IKcpExceptionProducer<T> producer, Action<Exception> handler)
    {
        if (producer is null) throw new ArgumentNullException(nameof(producer));
        if (handler is null) throw new ArgumentNullException(nameof(handler));

        producer.SetExceptionHandler(
            (ex, conv, state) =>
            {
                var handler = (Action<Exception>)state!;
                handler.Invoke(ex);
                return false;
            },
            handler
        );
    }
}