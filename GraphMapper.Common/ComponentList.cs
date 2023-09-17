using GraphMapper.Common.Components;
using System.Collections.Immutable;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace GraphMapper.Common;
public abstract class ComponentList<T> where T : IComponent
{
    private readonly Subject<T> _componentSubject = new();
    protected readonly List<T> _components = new();
    private readonly List<T> _pendingComponents = new();

    public IEnumerable<T> Components => _components.ToImmutableList();
    public IObservable<T> ComponentChanged => _componentSubject.AsObservable();

    public ComponentList() { }

    protected ComponentList(List<T> components) => _components = components;

    internal void AddComponent(T component)
    {
        if (!_components.Contains(component))
        {
            _pendingComponents.Add(component);
            _componentSubject.OnNext(component);
        }
    }

    internal void CommitChanges()
    {
        _components.AddRange(_pendingComponents);
        _pendingComponents.Clear();
    }

    internal void RollbackChanges()
    {
        _pendingComponents.Clear();
    }
}
