using GraphMapper.Common.Components;
using System.Collections.Immutable;

namespace GraphMapper.Common;
public class Graph : ComponentList, IHaveId
{
    private readonly List<Node> _nodes = new();
    private readonly List<Edge> _edges = new();

    public Guid Id { get; init; }
    public ImmutableList<Node> Nodes => _nodes.ToImmutableList();
    public ImmutableList<Edge> Edges => _edges.ToImmutableList();

    public void AddNode(Node node)
    {
        Components.Where(c => c.SupportedTypes.Contains(typeof(Node))).ForEach(c => node.AddComponent(c));

        _nodes.Add(node);
    }

    public void AddEdge(Node a, Node b)
    {
        if (!Nodes.Contains(a) || !Nodes.Contains(b))
            throw new InvalidOperationException("Nodes should contains in graph");

        AllowedPairedEdges? component = Components.SingleOrDefault(c => c.GetType() == typeof(AllowedPairedEdges)) as AllowedPairedEdges;

        int count = component is not null ? (int)component.Value.Value : 1;

        if (count == -1 || Edges.Count(e => (e.NodeA.Id == a.Id && e.NodeB.Id == b.Id) || (e.NodeA.Id == b.Id && e.NodeB.Id == a.Id)) < count)
        {
            Edge edge = new(a, b);
            Components.Where(c => c.SupportedTypes.Contains(typeof(Edge))).ForEach(c => edge.AddComponent(c));
            _edges.Add(edge);
        }
    }
}
