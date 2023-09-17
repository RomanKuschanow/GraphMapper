using GraphMapper.Common.Components;
using System.Collections.Immutable;

namespace GraphMapper.Common;
public class Node : ComponentList<INodeComponent>, IHaveId
{
    public Guid Id { get; init; }

    public Node()
    {
        Id = Guid.NewGuid();
    }

    private Node(List<INodeComponent> components) : base(components) { Id = Guid.NewGuid(); }

    internal Node Clone()
    {
        return new(_components);
    }
}
