using System.Collections.Generic;
using Liner.Core.Domain.Algorithms.Graphs.Architecture;

namespace Liner.Core.Domain.Algorithms.Graphs.BreadthFirstSearch
{
    public class BFS<TValue> : IPathFindAlgorithm<TValue>
    {
        public PathFindAlgorithmResponse<TValue> FindPath(Node<TValue> startNode, Node<TValue> endNode)
        {
            var queue = new Queue<Node<TValue>>();

            queue.Enqueue(startNode);

            while (queue.Count != 0)
            {
                var parent = queue.Dequeue();

                while (parent.Childrens.Count != 0)
                {
                    var children = parent.Childrens.Dequeue();

                    if (!children.Visited && !children.Unreachable)
                    {
                        children.SetVisited();
                        children.SetParent(parent);
                        queue.Enqueue(children);
                    }
                }
            }

            var nodesValues = new List<TValue>();

            var actualNode = endNode;

            if (!endNode.IsParentAvailable())
            {
                return BFSResponse<TValue>.PathNotFound;
            }

            while (actualNode.IsParentAvailable())
            {
                nodesValues.Add(actualNode.Value);
                actualNode = actualNode.Parent;

                if (actualNode == startNode)
                {
                    nodesValues.Add(startNode.Value);
                    break;
                }
            }

            return new BFSResponse<TValue>(true, nodesValues);
        }
    }
}
