using System.Collections.Generic;
using System.Linq;

namespace Liner.Core.Domain.Algorithms.Graphs.Architecture
{
    public abstract class PathFindAlgorithmResponse<TValue>
    {
        public TValue[] OrderedValuesFromStartToEnd { get; private set; }

        public PathFindAlgorithmResponse(IReadOnlyCollection<TValue> values)
        {
            OrderedValuesFromStartToEnd = values.ToArray();
        }
    }
}
