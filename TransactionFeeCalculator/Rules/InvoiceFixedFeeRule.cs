using System;
using System.Collections.Generic;

namespace TransactionFeeCalculator.Rules
{
    public class InvoiceFixedFeeRule : ITransactionRule
    {
        private Dictionary<string, DateTime> _merchantLastTransactionDate = new Dictionary<string, DateTime>();

        public decimal Fee { get; set; }

        public InvoiceFixedFeeRule(decimal fee)
        {
            Fee = fee;
        }

        public void Apply(Transaction transaction)
        {
            if (transaction.Fee <= 0) return;

            if (!_merchantLastTransactionDate.ContainsKey(transaction.MerchantName))
            {
                _merchantLastTransactionDate.Add(transaction.MerchantName, transaction.Date);

                transaction.Fee += Fee;

                return;
            }

            var lastTransactionDate = _merchantLastTransactionDate[transaction.MerchantName];

            bool addFee = lastTransactionDate.Month != transaction.Date.Month;

            if (addFee)
            {
                transaction.Fee += Fee;
                _merchantLastTransactionDate[transaction.MerchantName] = transaction.Date;
            }
        }
    }
}
