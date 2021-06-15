using System;

namespace Liner.Core.Domain
{
    //TwoPointLine
    public class Line
    {
        public Line(Point start, Point end)
        {
            Start = start ?? throw new ArgumentNullException(nameof(start));
            End = end ?? throw new ArgumentNullException(nameof(end));
        }

        public Point Start { get; set; }
        public Point End { get; set; }

        // check id two points are siblings
    }
}
