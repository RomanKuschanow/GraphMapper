using GraphMapper.Common.Components;
using GraphMapper.Common.Conditions;
using System.Collections.Immutable;

namespace GraphMapper.Common;
public class Graph : ComponentList<IGraphComponent>, IHaveId
{
    private readonly List<Node> _nodes = new();
    private readonly List<Edge> _edges = new();
    private readonly List<ICondition> _conditions = new();

    public Guid Id { get; init; }
    public ImmutableList<Node> Nodes => _nodes.ToImmutableList();
    public ImmutableList<Edge> Edges => _edges.ToImmutableList();
    public ImmutableList<ICondition> Conditions => _conditions.ToImmutableList();

    public Graph() => Id = Guid.NewGuid();
}
