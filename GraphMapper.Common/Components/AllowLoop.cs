using GraphMapper.Common.Local;
using System.Collections.Immutable;

namespace GraphMapper.Common.Components;
public class AllowLoop : IComponent
{
    public string Name => Resource.AllowLoopName;

    public ComponentValue Value => new(true);

    public ImmutableArray<Type> SupportedTypes => ImmutableArray.CreateRange(new[] { typeof(Edge), typeof(Graph) });

    public bool CanDuplicated => false;
}
