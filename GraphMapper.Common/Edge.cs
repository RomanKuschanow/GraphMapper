using GraphMapper.Common.Components;
using System.Collections.Immutable;

namespace GraphMapper.Common;
public class Edge : ComponentList<IEdgeComponent>, IHaveId
{
    public Guid Id { get; init; }

    public Node NodeA { get; private set; }
    public Node NodeB { get; private set; }

    internal Edge(Node nodeA, Node nodeB)
    {
        NodeA = nodeA;
        NodeB = nodeB;
    }

    private Edge(Node nodeA, Node nodeB, List<IEdgeComponent> components) : base(components)
    {
        NodeA = nodeA;
        NodeB = nodeB;
    }

    internal void ChangeNodes(Node a, Node b)
    {
        NodeA = a;
        NodeB = b;
    }

    internal Edge Clone()
    {
        return new(NodeA, NodeB, _components);
    }
}
