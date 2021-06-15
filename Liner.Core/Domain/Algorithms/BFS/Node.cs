using System.Collections.Generic;

namespace Liner.Core.Domain.Algorithms.BFS
{
    public class Node<T>
    {
        public T Value { get; private set; }
        public bool Visited { get; private set; }
        public bool Unavailable { get; private set; }
        public Node<T> Parent { get; private set; }
        public Queue<Node<T>> Childrens { get; private set; }

        public Node(T value)
        {
            Value = value;
            Visited = false;
            Unavailable = false;
            Childrens = new Queue<Node<T>>();
        }

        public void AddChildren(Node<T> children)
        {
            Childrens.Enqueue(children);
        }

        public void SetVisited() => Visited = true;

        public void SetUnavailable() => Unavailable = true;

        public bool IsParentAvailable() => Parent != null;

        public void SetParent(Node<T> parent)
        {
            Parent = parent;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
