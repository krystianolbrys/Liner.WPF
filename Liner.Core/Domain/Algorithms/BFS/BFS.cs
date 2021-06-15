using System.Collections.Generic;

namespace Liner.Core.Domain.Algorithms.BFS
{
    public class BFS<T>
    {
        public BFSResult<T> FindPath(Node<T> start, Node<T> end)
        {
            var queue = new Queue<Node<T>>();

            queue.Enqueue(start);

            while(queue.Count != 0)
            {
                var parent = queue.Dequeue();

                while(parent.Childrens.Count != 0)
                {
                    var children = parent.Childrens.Dequeue();

                    if(!children.Visited && !children.Unavailable)
                    {
                        children.SetVisited();
                        children.SetParent(parent);
                        queue.Enqueue(children);
                    }
                }
            }

            var nodesValues = new List<T>();

            var actualNode = end;

            if (!end.IsParentAvailable())
            {
                return BFSResult<T>.NotFound;
            }

            while (actualNode.IsParentAvailable())
            {
                nodesValues.Add(actualNode.Value);
                actualNode = actualNode.Parent;

                if(actualNode == start)
                {
                    nodesValues.Add(start.Value);
                    break;
                }
            }

            return new BFSResult<T>(true, nodesValues);
        }
    }
}
