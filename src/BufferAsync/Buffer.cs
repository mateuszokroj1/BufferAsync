using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BufferAsync
{
    public sealed class Buffer : IBuffer
    {
        #region Constructor

        public Buffer(long maxAvailableSize)
        {

        }

        public Buffer() : this(0) { }

        #endregion

        #region Fields

        private Span<byte> buffer = Span<byte>.Empty;

        private readonly SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        private readonly IDictionary<Guid, (int offset, int length)> blocksCollection = new Dictionary<Guid, (int offset, int length)>();

        #endregion

        #region Properties

        public long MaxAvailableSize { get; }

        public IEnumerable<KeyValuePair<Guid, Memory<byte>>> Blocks => blocksCollection.AsEnumerable();

        #endregion

        #region Methods

        public async ValueTask<Guid> CreateMemoryBlock(long size, bool initWithZeros = false)
        {

        }

        public async ValueTask<Guid> CreateMemoryBlockWhenIsAvailable()
        {

        }

        public async void Dispose() => await DisposeAsync();

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }

        #endregion
    }
}
