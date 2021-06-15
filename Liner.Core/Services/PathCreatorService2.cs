using System;
using System.Diagnostics;
using Liner.Core.Domain;
using Liner.Core.Domain.Algorithms.BFS;

namespace Liner.Core.Services
{
    public class PathCreatorService2
    {
        private readonly Point _start;
        private readonly Point _end;
        private readonly ExistingLines _existingLines;
        private readonly BoundaryPoint _boudaryPoint;

        public PathCreatorService2(Point start, Point end, ExistingLines existingLines, BoundaryPoint boudaryPoint)
        {
            _start = start ?? throw new ArgumentNullException(nameof(start));
            _end = end ?? throw new ArgumentNullException(nameof(end));
            _existingLines = existingLines ?? throw new ArgumentNullException(nameof(existingLines));
            _boudaryPoint = boudaryPoint ?? throw new ArgumentNullException(nameof(boudaryPoint));
        }

        public Path Create()
        {

            var width = _boudaryPoint.X;
            var height = _boudaryPoint.Y;

            var nodes = new Node<Point>[width, height];

            // create nodes
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    nodes[x, y] = new Node<Point>(new Point(x, y));
                }
            }

            // populate childrens
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    nodes[x, y].AddChildren(nodes[x, y - 1]);
                    nodes[x, y].AddChildren(nodes[x + 1, y]);
                    nodes[x, y].AddChildren(nodes[x, y + 1]);
                    nodes[x, y].AddChildren(nodes[x - 1, y]);
                }
            }

            var marginBetweenLines = 3;

            foreach (var line in _existingLines.Lines)
            {
                nodes[line.Start.X, line.Start.Y].SetUnavailable();
                nodes[line.End.X, line.End.Y].SetUnavailable();

                for (int x = line.Start.X - marginBetweenLines; x <= line.Start.X + marginBetweenLines; x++)
                {
                    for (int y = line.Start.Y - marginBetweenLines; y <= line.Start.Y + marginBetweenLines; y++)
                    {
                        nodes[x, y].SetUnavailable();
                    }
                }

                for (int x = line.End.X - marginBetweenLines; x <= line.End.X + marginBetweenLines; x++)
                {
                    for (int y = line.End.Y - marginBetweenLines; y <= line.End.Y + marginBetweenLines; y++)
                    {
                        nodes[x, y].SetUnavailable();
                    }
                }
            }

            var startNode = nodes[_start.X, _start.Y];
            var endNode = nodes[_end.X, _end.Y];

            var bfs = new BreadthFirstSearch<Point>();

            Stopwatch sw = Stopwatch.StartNew();

            var bfsResult = bfs.FindPath(startNode, endNode);

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);

            var response = new Path();

            for (int i = 0; i < bfsResult.Values.Length - 1; i++)
            {
                var line = new Line(bfsResult.Values[i], bfsResult.Values[i + 1]);
                response.AddLine(line);
            }

            return response;
        }
    }
}
