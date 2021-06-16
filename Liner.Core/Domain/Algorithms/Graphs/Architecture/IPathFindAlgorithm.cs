namespace Liner.Core.Domain.Algorithms.Graphs.Architecture
{
    public interface IPathFindAlgorithm<TValue>
    {
        PathFindAlgorithmResponse<TValue> FindPath(Node<TValue> startNode, Node<TValue> endNode);
    }
}
