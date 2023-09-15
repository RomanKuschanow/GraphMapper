using System.Collections.Immutable;

namespace GraphMapper.Common.Components;
public interface IComponent
{
    string Name { get; }
    object Value { get; }
    Type ValueType { get; }
}
