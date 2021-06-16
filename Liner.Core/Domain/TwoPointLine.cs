using System;
using Liner.Core.Domain.Exceptions;

namespace Liner.Core.Domain
{
    public class TwoPointLine
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }

        public TwoPointLine(Point start, Point end)
        {
            Start = start ?? throw new ArgumentNullException(nameof(start));
            End = end ?? throw new ArgumentNullException(nameof(end));

            ValidateIsTwoPointsAreSiblings(Start, End);
        }

        public void ValidateIsTwoPointsAreSiblings(Point start, Point end)
        {
            if (start.X == end.X)
            {
                if(!(Math.Abs(start.Y - end.Y) == 1))
                {
                    throw new TwoPointsAreNotSiblingsException();
                }
            }

            if (start.Y == end.Y)
            {
                if (!(Math.Abs(start.X - end.X) == 1))
                {
                    throw new TwoPointsAreNotSiblingsException();
                }
            }
        }
    }
}
