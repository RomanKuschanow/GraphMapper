using GraphMapper.Common.Components;

namespace GraphMapper.Common;
public class Node : ComponentList<INodeComponent>, IHaveId
{
    public Guid Id { get; init; }

    public Node() => Id = Guid.NewGuid();

    private Node(List<INodeComponent> components, Guid id) : base(components) => Id = id;

    internal Node Clone()
    {
        return new(_components, Id);
    }
}
