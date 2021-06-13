using System.Collections.Generic;
using Liner.API.Contracts.Common;

namespace Liner.API.Contracts.Requests
{
    public class GetPathRequest
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public ICollection<Line> ExistingLines { get; set; }
        public Boundaries Boundaries { get; set; }
    }
}
