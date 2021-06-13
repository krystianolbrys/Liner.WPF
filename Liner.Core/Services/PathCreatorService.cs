using System;
using Liner.Core.Domain;

namespace Liner.Core.Services
{
    public class PathCreatorService
    {
        private readonly Point _start;
        private readonly Point _end;
        private readonly ExistingLines _existingLines;
        private readonly BoundaryPoint _boudaryPoint;

        public PathCreatorService(Point start, Point end, ExistingLines existingLines, BoundaryPoint boudaryPoint)
        {
            _start = start ?? throw new ArgumentNullException(nameof(start));
            _end = end ?? throw new ArgumentNullException(nameof(end));
            _existingLines = existingLines ?? throw new ArgumentNullException(nameof(existingLines));
            _boudaryPoint = boudaryPoint ?? throw new ArgumentNullException(nameof(boudaryPoint));
        }

        public Path Create()
        {
            if (_existingLines.IsEmpty || true) // test
            {
                var path = new Path();

                var firstLine = new Line(new Point(_start.X, _start.Y), new Point(_start.X, _end.Y));
                var secondLine = new Line(new Point(_start.X, _end.Y), new Point(_end.X, _end.Y));

                path.AddLine(firstLine);
                path.AddLine(secondLine);

                return path;
            }

            return new Path();
        }
    }
}
