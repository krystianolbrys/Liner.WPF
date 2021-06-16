using System;
using Liner.Core.Domain.Algorithms.Graphs.Architecture;

namespace Liner.Core.Domain
{
    public class PointNodes
    {
        public readonly Point Start;
        public readonly Point End;
        public readonly ExistingLines ExistingLines;
        public readonly Configuration Configuration;
        public Node<Point>[,] Nodes { get; private set; }

        public PointNodes(Point start, Point end, ExistingLines existingLines, Configuration configuration)
        {
            Start = start ?? throw new ArgumentNullException(nameof(start));
            End = end ?? throw new ArgumentNullException(nameof(end));
            ExistingLines = existingLines ?? throw new ArgumentNullException(nameof(existingLines));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public PointNodes Prepare()
        {
            CreateAndInitializeNodesTable();
            AssignNearesHorizontalAndVerticalNeighoursAsChildrens();
            SetNodesAsUnreachableBasedOnExistingLinesAndMargin();
            return this;
        }

        public Node<Point> GetStartNode => GetNodeBasedOnCordinates(Start);

        public Node<Point> GetEndNode => GetNodeBasedOnCordinates(End);

        private void CreateAndInitializeNodesTable()
        {
            Nodes = new Node<Point>[Configuration.Width, Configuration.Height];

            for (int x = 0; x < Configuration.Width; x++)
            {
                for (int y = 0; y < Configuration.Height; y++)
                {
                    Nodes[x, y] = new Node<Point>(new Point(x, y));
                }
            }
        }

        private void AssignNearesHorizontalAndVerticalNeighoursAsChildrens()
        {
            for (int x = 1; x < Configuration.Width - 1; x++)
            {
                for (int y = 1; y < Configuration.Height - 1; y++)
                {
                    Nodes[x, y].AddChildren(Nodes[x, y - 1]);
                    Nodes[x, y].AddChildren(Nodes[x + 1, y]);
                    Nodes[x, y].AddChildren(Nodes[x, y + 1]);
                    Nodes[x, y].AddChildren(Nodes[x - 1, y]);
                }
            }
        }

        private void SetNodesAsUnreachableBasedOnExistingLinesAndMargin()
        {
            var marginInPixels = Configuration.LineMargin.Value;

            foreach (var line in ExistingLines.TwoPointLines)
            {
                Nodes[line.Start.X, line.Start.Y].SetUnreachable();
                Nodes[line.End.X, line.End.Y].SetUnreachable();

                for (int x = line.Start.X - marginInPixels; x <= line.Start.X + marginInPixels; x++)
                {
                    for (int y = line.Start.Y - marginInPixels; y <= line.Start.Y + marginInPixels; y++)
                    {
                        if (IsPixelCordinatesInBoundaries(x, y, Configuration))
                        {
                            Nodes[x, y].SetUnreachable();
                        }
                    }
                }

                for (int x = line.End.X - marginInPixels; x <= line.End.X + marginInPixels; x++)
                {
                    for (int y = line.End.Y - marginInPixels; y <= line.End.Y + marginInPixels; y++)
                    {
                        if (IsPixelCordinatesInBoundaries(x, y, Configuration))
                        {
                            Nodes[x, y].SetUnreachable();
                        }
                    }
                }
            }
        }

        private Node<Point> GetNodeBasedOnCordinates(Point point)
        {
            if (!IsPixelCordinatesInBoundaries(point.X, point.Y, Configuration))
            {
                throw new ArgumentOutOfRangeException($"PixelCordinatesInBoundaries not meet - Here json with all request data");
            }

            return Nodes[point.X, point.Y];
        }

        private bool IsPixelCordinatesInBoundaries(int x, int y, Configuration configuration) =>
            x >= 0
            && x < configuration.Width
            && y >= 0
            && y < configuration.Height;
    }
}
