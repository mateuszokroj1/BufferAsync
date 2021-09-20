using System;
using System.Runtime.InteropServices;

namespace BufferAsync.Unsafe
{
    public static unsafe class BufferHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <exception cref="OutOfMemoryException"/>
        public static Memory<byte> CreateFirstBlock(int size)
        {
            var pointer = Marshal.AllocHGlobal(size);

            
        }

        public static void InitBlock(ref Memory<byte> block)
        {
            if (block.IsEmpty)
                throw new ArgumentException("Memory is empty.");

            var handle = block.Pin();
            System.Runtime.CompilerServices.Unsafe.InitBlock(handle.Pointer, 0, (uint)block.Length);
        }
    }
}
