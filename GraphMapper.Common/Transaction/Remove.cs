#nullable disable

namespace GraphMapper.Common.Transaction;
public class Remove : ITransactionAct
{
    public object Obj { get; }
    public Type Type { get; }

    public Remove(object obj)
    {
        Obj = obj;
        Type = obj.GetType();
    }
}
