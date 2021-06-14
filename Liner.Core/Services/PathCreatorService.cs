using System;
using System.Collections.Generic;
using System.Linq;
using Liner.Core.Domain;
using Liner.Core.Domain.BFS;

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

            var width = _boudaryPoint.X;
            var height = _boudaryPoint.Y;

            var nodes = new BFSNode<Point>[width, height];

            // create nodes
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    nodes[x, y] = new BFSNode<Point>(new Point(x, y));
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
                    nodes[x, y].FinalizeChildrens();
                }
            }

            foreach (var line in _existingLines.Lines)
            {
                nodes[line.Start.X, line.Start.Y].SetUnavailable();
                nodes[line.End.X, line.End.Y].SetUnavailable();
            }

            // BFS start
            var bfsList = new List<BFSNode<Point>>();

            var sourceNode = nodes[_start.X, _start.Y];
            var endNode = nodes[_end.X, _end.Y];

            //return new Path();

            bfsList.Add(sourceNode);

            while (bfsList.Any())
            {
                var node = bfsList.FirstOrDefault(n => !n.Visited);

                if (node == null)
                {
                    break;
                }
                node.Visited = true;

                //for (int i = 0; i < node.ChildrensArray.Length; i++)
                //{
                //    if (!node.ChildrensArray[i].Visited && !node.ChildrensArray[i].Unavailable)
                //    {
                //        //if (!bfsList.Contains(node.ChildrensArray[i]))
                //        //{
                //        //    bfsList.Add(node.ChildrensArray[i]);
                //        //}
                //        bfsList.Add(node.ChildrensArray[i]);

                //        node.ChildrensArray[i].Parent = node;
                //    }
                //}

                foreach (var children in node.ChildrensArray)
                {
                    if (!children.Visited && !children.Unavailable)
                    {
                        if (!bfsList.Contains(children))
                        {
                            bfsList.Add(children);
                        }

                        children.SetParent(node);
                    }
                }
            }

            return new Path();

            var solution = new List<Point>();

            var prev = endNode;

            solution.Add(prev.Value);

            while(prev != sourceNode)
            {
                solution.Add(prev.Parent.Value);
                prev = prev.Parent;
            }

            var pathNarysowany = new Path();
            var responseLines = new List<Line>();

            for (int i = 0; i < solution.Count -1; i++)
            {
                var line = new Line(solution[i], solution[i + 1]);
                pathNarysowany.AddLine(line);
            }

            return pathNarysowany;
        }
    }
}
