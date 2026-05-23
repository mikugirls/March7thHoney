using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace March7thHoney.Kcp.KcpSharp;




public readonly struct KcpRentedBuffer : IEquatable<KcpRentedBuffer>, IDisposable
{
    private readonly Memory<byte> _memory;

    internal object? Owner { get; }

    
    
    
    public Memory<byte> Memory => _memory;

    
    
    
    public Span<byte> Span => _memory.Span;

    
    
    
    public bool IsAllocated => Owner is not null;

    
    
    
    public bool IsEmpry => _memory.IsEmpty;

    internal KcpRentedBuffer(object? owner, Memory<byte> buffer)
    {
        Owner = owner;
        _memory = buffer;
    }

    
    
    
    
    
    public static KcpRentedBuffer FromMemory(Memory<byte> memory)
    {
        return new KcpRentedBuffer(null, memory);
    }

    
    
    
    
    
    public static KcpRentedBuffer FromSharedArrayPool(int size)
    {
        if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
        var buffer = ArrayPool<byte>.Shared.Rent(size);
        return new KcpRentedBuffer(ArrayPool<byte>.Shared, buffer);
    }

    
    
    
    
    
    
    public static KcpRentedBuffer FromArrayPool(ArrayPool<byte> pool, byte[] buffer)
    {
        if (pool is null) throw new ArgumentNullException(nameof(pool));
        if (buffer is null) throw new ArgumentNullException(nameof(buffer));
        return new KcpRentedBuffer(pool, buffer);
    }

    
    
    
    
    
    
    public static KcpRentedBuffer FromArrayPool(ArrayPool<byte> pool, ArraySegment<byte> arraySegment)
    {
        if (pool is null) throw new ArgumentNullException(nameof(pool));
        return new KcpRentedBuffer(pool, arraySegment);
    }

    
    
    
    
    
    
    public static KcpRentedBuffer FromArrayPool(ArrayPool<byte> pool, int size)
    {
        if (pool is null) throw new ArgumentNullException(nameof(pool));
        if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
        return new KcpRentedBuffer(pool, pool.Rent(size));
    }

    
    
    
    
    
    public static KcpRentedBuffer FromMemoryOwner(IMemoryOwner<byte> memoryOwner)
    {
        if (memoryOwner is null) throw new ArgumentNullException(nameof(memoryOwner));
        return new KcpRentedBuffer(memoryOwner, memoryOwner.Memory);
    }


    
    
    
    
    
    
    public static KcpRentedBuffer FromMemoryOwner(IDisposable memoryOwner, Memory<byte> memory)
    {
        if (memoryOwner is null) throw new ArgumentNullException(nameof(memoryOwner));
        return new KcpRentedBuffer(memoryOwner, memory);
    }

    
    
    
    
    
    public KcpRentedBuffer Slice(int start)
    {
        var memory = _memory;
        if ((uint)start > (uint)memory.Length) ThrowHelper.ThrowArgumentOutOfRangeException(nameof(start));
        return new KcpRentedBuffer(Owner, memory.Slice(start));
    }

    
    
    
    
    
    
    
    
    
    public KcpRentedBuffer Slice(int start, int length)
    {
        var memory = _memory;
        if ((uint)start > (uint)memory.Length) ThrowHelper.ThrowArgumentOutOfRangeException(nameof(start));
        if ((uint)length > (uint)(memory.Length - start)) ThrowHelper.ThrowArgumentOutOfRangeException(nameof(length));
        return new KcpRentedBuffer(Owner, memory.Slice(start, length));
    }

    
    public void Dispose()
    {
        Debug.Assert(Owner is null || Owner is ArrayPool<byte> || Owner is IDisposable);

        if (Owner is null) return;
        if (Owner is ArrayPool<byte> arrayPool)
            if (MemoryMarshal.TryGetArray(_memory, out ArraySegment<byte> arraySegment))
            {
                arrayPool.Return(arraySegment.Array!);
                return;
            }

        if (Owner is IDisposable disposable) disposable.Dispose();
    }

    
    public bool Equals(KcpRentedBuffer other)
    {
        return ReferenceEquals(Owner, other.Owner) && _memory.Equals(other._memory);
    }

    
    public override bool Equals(object? obj)
    {
        return obj is KcpRentedBuffer other && Equals(other);
    }

    
    public override int GetHashCode()
    {
        return Owner is null ? _memory.GetHashCode() : HashCode.Combine(RuntimeHelpers.GetHashCode(Owner), _memory);
    }

    
    public override string ToString()
    {
        return $"KcpSharp.KcpRentedBuffer[{_memory.Length}]";
    }
}