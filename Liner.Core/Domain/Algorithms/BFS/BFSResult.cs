using System.Collections.Generic;
using System.Linq;

namespace Liner.Core.Domain.Algorithms.BFS
{
    public class BFSResult<T>
    {
        public bool Success { get; private set; }
        public T[] Values { get; private set; }

        public BFSResult(bool success, IEnumerable<T> values)
        {
            Success = success;
            Values = values.ToArray();
        }

        public static BFSResult<T> NotFound =>
            new BFSResult<T>(false, new List<T>());
    }
}
