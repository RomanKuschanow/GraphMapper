using System.Collections.Immutable;

namespace GraphMapper.Common.Components;
public abstract class ComponentList<T> where T : IComponent
{
    protected readonly List<T> _components = new();
    private readonly List<T> _addingComponents = new();
    private readonly List<T> _removingComponents = new();

    public ImmutableList<T> Components => _components.ToImmutableList();

    public ComponentList() { }

    protected ComponentList(List<T> components) => _components = components;

    public void AddComponent(T component)
    {
        if (!_components.Contains(component) && !_addingComponents.Contains(component))
            _addingComponents.Add(component);
    }

    public void RemoveComponent(T component)
    {
        if (_components.Contains(component) && !_removingComponents.Contains(component))
            _removingComponents.Add(component);
    }

    internal virtual void CommitChanges()
    {
        _components.AddRange(_addingComponents);
        _components.RemoveAll(t => _removingComponents.Contains(t));
        DismissChanges();
    }

    public void DismissChanges()
    {
        _addingComponents.Clear();
        _removingComponents.Clear();
    }
}
