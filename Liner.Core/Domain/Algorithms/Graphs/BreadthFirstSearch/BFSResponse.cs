using System.Collections.Generic;
using System.Linq;
using Liner.Core.Domain.Algorithms.Graphs.Architecture;

namespace Liner.Core.Domain.Algorithms.Graphs.BreadthFirstSearch
{
    public class BFSResponse<TValue> : PathFindAlgorithmResponse<TValue>
    {
        public BFSResponse(IReadOnlyCollection<TValue> values)
            : base(values)
        {
        }

        public static BFSResponse<TValue> PathNotFound =>
            new BFSResponse<TValue>(EmptyCollection);

        private static IReadOnlyCollection<TValue> EmptyCollection =>
            new List<TValue>().ToList().AsReadOnly();
    }
}
