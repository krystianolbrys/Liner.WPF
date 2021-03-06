using System.Collections.Generic;
using Liner.API.Contracts.Common;

namespace Liner.API.Contracts.Requests
{
    public class GetPathRequest
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public ICollection<TwoPointLine> ExistingLines { get; set; }
        public Configuration Configuration { get; set; }
    }
}
