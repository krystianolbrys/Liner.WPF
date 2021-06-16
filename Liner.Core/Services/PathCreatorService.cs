using System;
using Liner.Core.Domain;
using Liner.Core.Domain.Algorithms.Graphs.Architecture;
using Liner.Core.Domain.Algorithms.Graphs.BreadthFirstSearch;

namespace Liner.Core.Services
{
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
            var nodes = 
                new PointNodes(_start, _end, _existingLines, _configuration)
                .Prepare();

            // if there will be more algorithms
            // they shoud be privded by constructor to this service explicitly
            // by some kind of provider for strategy selection based on problem to solve
            IPathFindAlgorithm<Point> searchAlgorithm = 
                new BFS<Point>();

            var algorithmResult = 
                searchAlgorithm.FindPath(nodes.GetStartNode, nodes.GetEndNode);

            var response = new Path();

            for (int i = 0; i < algorithmResult.OrderedValuesFromStartToEnd.Length - 1; i++)
            {
                var line = new TwoPointLine(algorithmResult.OrderedValuesFromStartToEnd[i], algorithmResult.OrderedValuesFromStartToEnd[i + 1]);
                response.AddLine(line);
            }

            return response;
        }
    }
}
