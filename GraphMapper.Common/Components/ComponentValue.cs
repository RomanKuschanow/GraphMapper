namespace GraphMapper.Common.Components;

public class ComponentValue
{
    public Type Type { get; init; }
    public object Value { get; private set; }

    public ComponentValue(object value)
    {
        Type = value.GetType();
        Value = value;
    }

    public void SetValue(object value)
    {
        if (!value.GetType().Equals(Type))
            throw new InvalidOperationException($"The type of value should be {Type}, but found {value.GetType()}");

        Value = value;
    }
}