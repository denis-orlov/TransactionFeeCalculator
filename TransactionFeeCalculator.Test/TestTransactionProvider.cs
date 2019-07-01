using System;
using System.Collections.Generic;

namespace TransactionFeeCalculator.Test
{
    public class TestTransactionProvider : ITransactionProvider
    {
        private Func<IEnumerable<Transaction>> getTransactionsAction;

        public TestTransactionProvider(Func<IEnumerable<Transaction>> action)
        {
            this.getTransactionsAction = action;
        }
        
        public IEnumerable<Transaction> GetTransactions()
        {
            return getTransactionsAction();
        }
    }
}
