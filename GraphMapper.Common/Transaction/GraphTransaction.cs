using System.Collections.Immutable;

namespace GraphMapper.Common.Transaction;
public class GraphTransaction
{
    private readonly List<ITransactionAct> _transactionActs = new();

    public ImmutableList<ITransactionAct> TransactionActs => _transactionActs.ToImmutableList();

    public void AddAction(ITransactionAct act)
    {
        if (!_transactionActs.Contains(act))
            _transactionActs.Add(act);
    }
}
