using System.Collections.Immutable;

namespace GraphMapper.Common.Components;
public interface IComponent
{
    string Name { get; }
    ComponentValue Value { get; }
    ImmutableArray<Type> SupportedTypes { get; }
    bool CanDuplicated { get; }
}
