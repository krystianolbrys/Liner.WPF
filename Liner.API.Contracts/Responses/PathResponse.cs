using System.Collections.Generic;
using Liner.API.Contracts.Common;

namespace Liner.API.Contracts.Responses
{
    public class PathResponse
    {
        public IReadOnlyCollection<Line> Lines { get; set; }
    }
}
