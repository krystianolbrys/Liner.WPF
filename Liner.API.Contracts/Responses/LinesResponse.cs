using System.Collections.Generic;

namespace Liner.API.Contracts.Responses
{
    public class LinesResponse
    {
        public IReadOnlyCollection<Line> Lines { get; set; }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Line
    {
        public Point Start { get; set; }
        public Point End { get; set; }
    }
}
