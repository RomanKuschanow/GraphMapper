using GraphMapper.Common.Components;
using System.Collections.Immutable;

namespace GraphMapper.Common;
public abstract class ComponentList
{
    private readonly List<IComponent> _components = new();

    public ImmutableList<IComponent> Components => _components.ToImmutableList();

    public void AddComponent(IComponent component)
    {
        if (!_components.Contains(component) && component.SupportedTypes.Contains(GetType()) && (component.CanDuplicated || !_components.Select(c => c.GetType()).Contains(component.GetType())))
            _components.Add(component);
    }
}
