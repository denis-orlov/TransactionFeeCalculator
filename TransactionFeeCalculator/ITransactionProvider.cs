using System.Collections.Generic;

namespace TransactionFeeCalculator
{
    public interface ITransactionProvider
    {
        IEnumerable<Transaction> GetTransactions();
    }
}
