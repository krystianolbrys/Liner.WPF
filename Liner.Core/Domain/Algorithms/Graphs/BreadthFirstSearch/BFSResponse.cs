using System.Collections.Generic;
using System.Linq;
using Liner.Core.Domain.Algorithms.Graphs.Architecture;

namespace Liner.Core.Domain.Algorithms.Graphs.BreadthFirstSearch
{
    public class BFSResponse<TValue> : PathFindAlgorithmResponse<TValue>
    {
        public BFSResponse(bool success, IReadOnlyCollection<TValue> values)
            : base(success, values)
        {
        }

        public static BFSResponse<TValue> PathNotFound =>
            new BFSResponse<TValue>(false, new List<TValue>().ToList().AsReadOnly());
    }
}
