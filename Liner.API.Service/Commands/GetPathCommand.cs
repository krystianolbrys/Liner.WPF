using System.Collections.Generic;
using Liner.API.Contracts.Responses;
using MediatR;

namespace Liner.API.Service.Commands
{
    public class GetPathCommand : IRequest<PathResponse>
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public ICollection<TwoPointLine> ExistingLines { get; set; }
        public Configuration Configuration { get; set; }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class TwoPointLine
    {
        public Point Start { get; set; }
        public Point End { get; set; }
    }

    public class Configuration
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int LineMarginInPixels { get; set; }
    }
}
