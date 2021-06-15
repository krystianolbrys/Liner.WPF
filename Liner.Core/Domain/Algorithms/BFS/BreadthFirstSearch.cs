using System.Collections.Generic;
using System.Linq;

namespace Liner.Core.Domain.Algorithms.BFS
{
    public class BreadthFirstSearch<T>
    {
        public BreadthFirstSearchResult<T> FindPath(Node<T> start, Node<T> end)
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
                return BreadthFirstSearchResult<T>.NotFound;
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

            return new BreadthFirstSearchResult<T>(true, nodesValues);
        }
    }

    public class BreadthFirstSearchResult<T>
    {
        public bool Success { get; private set; }
        public T[] Values { get; private set; }

        public BreadthFirstSearchResult(bool success, IEnumerable<T> values)
        {
            Success = success;
            Values = values.ToArray();
        }

        public static BreadthFirstSearchResult<T> NotFound =>
            new BreadthFirstSearchResult<T>(false, new List<T>());
    }
}
