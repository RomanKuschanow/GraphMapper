using GraphMapper.Common.Components;

namespace GraphMapper.Common;
public class Edge : ComponentList<IEdgeComponent>, IHaveId
{
    public Guid Id { get; init; }

    private Node _newNodeA;
    private Node _newNodeB;

    public Node NodeA { get; private set; }
    public Node NodeB { get; private set; }

    internal Edge(Node nodeA, Node nodeB)
    {
        NodeA = nodeA;
        NodeB = nodeB;
        _newNodeA = nodeA;
        _newNodeB = nodeB;
        Id = Guid.NewGuid();
    }

    private Edge(Node nodeA, Node nodeB, List<IEdgeComponent> components, Guid id) : base(components)
    {
        NodeA = nodeA;
        NodeB = nodeB;
        _newNodeA = nodeA;
        _newNodeB = nodeB;
        Id = id;
    }

    internal void ChangeNodes(Node a, Node b)
    {
        _newNodeA = a;
        _newNodeB = b;
    }

    internal override void CommitChanges()
    {
        base.CommitChanges();

        NodeA = _newNodeA;
        NodeB = _newNodeB;
    }

    internal Edge Clone()
    {
        return new(NodeA, NodeB, _components, Id);
    }
}
