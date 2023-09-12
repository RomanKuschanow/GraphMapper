using GraphMapper.Common.Local;
using System.Collections.Immutable;

namespace GraphMapper.Common.Components;
public class AllowedPairedEdges : IComponent
{
    public string Name => Resource.AllowedPairedEdgesName;

    public ComponentValue Value => new(-1);

    public ImmutableArray<Type> SupportedTypes => ImmutableArray.CreateRange(new[] {typeof(Graph)});

    public bool CanDuplicated => false;

    public AllowedPairedEdges(int value)
    {
        if (value < 0)
            value = -1;

        Value.SetValue(value); 
    }
}
