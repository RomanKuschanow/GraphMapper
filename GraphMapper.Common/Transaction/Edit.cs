#nullable disable
using GraphMapper.Common.Components;

namespace GraphMapper.Common.Transaction;
public class Edit<T> : ITransactionAct where T : ComponentList<IComponent>
{
    public object Obj { get; }
    public Type Type { get; }
    public Action<T>[] Actions { get; }

    public Edit(T obj, params Action<T>[] actions)
    {
        Obj = obj;
        Type = obj.GetType();
        Actions = actions;
    }

}
