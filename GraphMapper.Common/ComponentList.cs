using GraphMapper.Common.Components;
using System.Collections.Immutable;

namespace GraphMapper.Common;
public abstract class ComponentList<T> where T : IComponent
{
    private readonly List<T> _components = new();

    public ImmutableList<T> Components => _components.ToImmutableList();

    public void AddComponent(T component)
    {
        if (!_components.Contains(component))
            _components.Add(component);
    }
}
