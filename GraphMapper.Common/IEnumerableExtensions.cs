namespace GraphMapper.Common;
internal static class IEnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
    {
        foreach (var value in values)
            action(value);
    }
}
