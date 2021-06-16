using System.Collections.Generic;
using System.Linq;

namespace Liner.Core.Domain.Algorithms.Graphs.Architecture
{
    public abstract class PathFindAlgorithmResponse<TValue>
    {
        public bool Success { get; private set; }
        public TValue[] OrderedValuesFromStartToEnd { get; private set; }

        public PathFindAlgorithmResponse(bool success, IReadOnlyCollection<TValue> values)
        {
            Success = success;
            OrderedValuesFromStartToEnd = values.ToArray();
        }
    }
}
