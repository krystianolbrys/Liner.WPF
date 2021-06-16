using System.Collections.Generic;
using Liner.API.Contracts.Common;

namespace Liner.API.Contracts.Responses
{
    public class PathResponse
    {
        public bool IsPathFound { get; set; }
        public IReadOnlyCollection<TwoPointLine> TwoPointLines { get; set; }

        static public PathResponse EmptyResponse =>
            new PathResponse 
            { 
                IsPathFound = false,
                TwoPointLines = new List<TwoPointLine>().AsReadOnly()
            };
    }
}
