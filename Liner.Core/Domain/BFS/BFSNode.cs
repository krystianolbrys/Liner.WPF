using System;
using System.Collections.Generic;
using System.Linq;

namespace Liner.Core.Domain.BFS
{
    public class BFSNode<T>
    {
        public bool Unavailable { get; set; }
        public bool Visited { get; set; }
        public BFSNode<T> Parent { get; set; }

        public T Value { get; private set; }
        public ICollection<BFSNode<T>> Childrens { get; private set; }

        public BFSNode<T>[] ChildrensArray { get; private set; }

        public BFSNode(T value)
        {
            Value = value;
            Childrens = new List<BFSNode<T>>();

            Unavailable = false;
            Visited = false;
            Parent = null;

            ChildrensArray = Array.Empty<BFSNode<T>>();
        }

        public void AddChildren(BFSNode<T> node)
        {
            Childrens.Add(node);
        }

        public void FinalizeChildrens()
        {
            ChildrensArray = Childrens.ToArray();
        }

        public void SetUnavailable() => Unavailable = true;

        public void SetVisited() => Visited = true;

        public void SetParent(BFSNode<T> parent) => Parent = parent;

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
