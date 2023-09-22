#nullable disable


namespace GraphMapper.Common.Transaction;
public class Add : ITransactionAct
{
    public object Obj { get; }
    public Type Type { get; }

    public Add(object obj)
    {
        Obj = obj;
        Type = obj.GetType();
    }
}
