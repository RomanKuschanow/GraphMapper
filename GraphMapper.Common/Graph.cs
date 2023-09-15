using GraphMapper.Common.Components;
using System.Collections.Immutable;

namespace GraphMapper.Common;
public class Graph : IHaveId
{
    private readonly List<Node> _nodes = new();
    private readonly List<Edge> _edges = new();

    public Guid Id { get; init; }
    public ImmutableList<Node> Nodes => _nodes.ToImmutableList();
    public ImmutableList<Edge> Edges => _edges.ToImmutableList();

    public void AddNode(Node node)
    {
        _nodes.Add(node);
    }

    public void AddEdge(Node a, Node b)
    {
        _edges.Add(new(a, b));
    }
}
