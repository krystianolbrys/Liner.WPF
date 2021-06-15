using System;
using Liner.Core.Domain;
using Liner.Core.Domain.Algorithms.BFS;

namespace Liner.Core.Services
{
    public class BFSPointNodesProvider
    {
        private readonly Point _start;
        private readonly Point _end;
        private readonly ExistingLines _existingLines;
        private readonly Configuration _configuration;
        private Node<Point>[,] _nodes;

        public BFSPointNodesProvider(Point start, Point end, ExistingLines existingLines, Configuration configuration)
        {
            _start = start ?? throw new ArgumentNullException(nameof(start));
            _end = end ?? throw new ArgumentNullException(nameof(end));
            _existingLines = existingLines ?? throw new ArgumentNullException(nameof(existingLines));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Node<Point>[,] Provide()
        {
            CreateAndInitializeNodesTable(_configuration);
            AssignNearesHorizontalAndVerticalNeighoursAsChildrens(_nodes, _configuration);
            SetNodesAsUnreachableBasedOnExistingLinesAndMargin(_nodes, _existingLines, _configuration);

            return _nodes;
        }

        public Node<Point> GetStartNode => GetNodeBasedOnCordinates(_nodes, _start, _configuration);

        public Node<Point> GetEndNode => GetNodeBasedOnCordinates(_nodes, _end, _configuration);

        private void CreateAndInitializeNodesTable(Configuration configuration)
        {
            _nodes = new Node<Point>[configuration.Width, configuration.Height];

            for (int x = 0; x < configuration.Width; x++)
            {
                for (int y = 0; y < configuration.Height; y++)
                {
                    _nodes[x, y] = new Node<Point>(new Point(x, y));
                }
            }
        }

        private void AssignNearesHorizontalAndVerticalNeighoursAsChildrens(Node<Point>[,] nodes, Configuration configuration)
        {
            for (int x = 1; x < configuration.Width - 1; x++)
            {
                for (int y = 1; y < configuration.Height - 1; y++)
                {
                    nodes[x, y].AddChildren(nodes[x, y - 1]);
                    nodes[x, y].AddChildren(nodes[x + 1, y]);
                    nodes[x, y].AddChildren(nodes[x, y + 1]);
                    nodes[x, y].AddChildren(nodes[x - 1, y]);
                }
            }
        }

        private void SetNodesAsUnreachableBasedOnExistingLinesAndMargin(Node<Point>[,] nodes, ExistingLines existingLines, Configuration configuration)
        {
            var marginInPixels = configuration.LineMargin.Value;

            foreach (var line in existingLines.TwoPointLines)
            {
                nodes[line.Start.X, line.Start.Y].SetUnreachable();
                nodes[line.End.X, line.End.Y].SetUnreachable();

                for (int x = line.Start.X - marginInPixels; x <= line.Start.X + marginInPixels; x++)
                {
                    for (int y = line.Start.Y - marginInPixels; y <= line.Start.Y + marginInPixels; y++)
                    {
                        if (IsPixelCordinatesInBoundaries(x, y, configuration))
                        {
                            nodes[x, y].SetUnreachable();
                        }
                    }
                }

                for (int x = line.End.X - marginInPixels; x <= line.End.X + marginInPixels; x++)
                {
                    for (int y = line.End.Y - marginInPixels; y <= line.End.Y + marginInPixels; y++)
                    {
                        if (IsPixelCordinatesInBoundaries(x, y, configuration))
                        {
                            nodes[x, y].SetUnreachable();
                        }
                    }
                }
            }
        }

        private Node<Point> GetNodeBasedOnCordinates(Node<Point>[,] nodes, Point point, Configuration configuration)
        {
            if (!IsPixelCordinatesInBoundaries(point.X, point.Y, configuration))
            {
                throw new ArgumentOutOfRangeException($"PixelCordinatesInBoundaries not meet - Here json with all request data");
            }

            return nodes[point.X, point.Y];
        }

        private bool IsPixelCordinatesInBoundaries(int x, int y, Configuration configuration) =>
            x >= 0
            && x < configuration.Width
            && y >= 0
            && y < configuration.Height;
    }

    public class PathCreatorService
    {
        private readonly Point _start;
        private readonly Point _end;
        private readonly ExistingLines _existingLines;
        private readonly Configuration _configuration;

        public PathCreatorService(Point start, Point end, ExistingLines existingLines, Configuration configuration)
        {
            _start = start ?? throw new ArgumentNullException(nameof(start));
            _end = end ?? throw new ArgumentNullException(nameof(end));
            _existingLines = existingLines ?? throw new ArgumentNullException(nameof(existingLines));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Path Create()
        {
            var nodesProvider = new BFSPointNodesProvider(_start, _end, _existingLines, _configuration);

            nodesProvider.Provide();

            var searchAlgorithm = new BFS<Point>();

            var bfsResult = searchAlgorithm.FindPath(nodesProvider.GetStartNode, nodesProvider.GetEndNode);

            var response = new Path();

            for (int i = 0; i < bfsResult.Values.Length - 1; i++)
            {
                var line = new TwoPointLine(bfsResult.Values[i], bfsResult.Values[i + 1]);
                response.AddLine(line);
            }

            return response;
        }
    }
}
