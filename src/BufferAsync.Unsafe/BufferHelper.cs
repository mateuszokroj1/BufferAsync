using System;
using System.Runtime.InteropServices;

namespace BufferAsync.Unsafe
{
    public static unsafe class BufferHelper
    {
        /// <summary>
        /// Declares new byte block in unmanaged memory
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <exception cref="OutOfMemoryException"/>
        /// <exception cref="ArgumentOutOfRangeException" />
        public static Span<byte> CreateFirstBlock(int size)
        {
            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size));

            var pointer = Marshal.AllocHGlobal(size);

            return new Span<byte>(pointer.ToPointer(), size);
        }

        /// <summary>
        /// Resizes block of unmanaged memory.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <exception cref="OutOfMemoryException" />
        public static Span<byte> ResizeBlock(Span<byte> block, int size)
        {
            var pointer = System.Runtime.CompilerServices.Unsafe.AsPointer(ref MemoryMarshal.GetReference(block));

            if (size == block.Length)
                return block;

            var newPointer = Marshal.ReAllocHGlobal(new IntPtr(pointer), new IntPtr(size));

            return new Span<byte>(newPointer.ToPointer(), size);
        }

        /// <summary>
        /// Initializes block with 0 value.
        /// </summary>
        /// <param name="block"></param>
        public static void InitBlock(Span<byte> block)
        {
            if (block.IsEmpty)
                throw new ArgumentException("Memory is empty.");

            System.Runtime.CompilerServices.Unsafe.InitBlock(ref MemoryMarshal.GetReference(block), 0, (uint)block.Length);
        }

        public static void ReleaseBlock(Span<byte> block)
        {
            var pointer = System.Runtime.CompilerServices.Unsafe.AsPointer(ref MemoryMarshal.GetReference(block));

            Marshal.FreeHGlobal(new IntPtr(pointer));
        }
    }
}
