using System.Collections.Generic;

namespace TransactionFeeCalculator.Test
{
    public class TestTransactionConsumer : ITransactionConsumer
    {
        public List<Transaction> Transactions { get; } = new List<Transaction>();

        public void ConsumeTransaction(Transaction tran)
        {
            Transactions.Add(tran);
        }
    }
}
