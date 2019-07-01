using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TransactionFeeCalculator.Rules;

namespace TransactionFeeCalculator.Test
{
    [TestClass]
    public class InvoiceFixedFeeRuleTest
    {
        private IEnumerable<Transaction> TestTransactionsWithoutFee()
        {
            yield return new Transaction(new DateTime(2019, 05, 01), "merchant1", 100);
            yield return new Transaction(new DateTime(2019, 06, 01), "merchant1", 100);
            yield return new Transaction(new DateTime(2019, 06, 06), "merchant1", 100);
            yield return new Transaction(new DateTime(2019, 06, 01), "merchant2", 100);
            yield return new Transaction(new DateTime(2019, 06, 06), "merchant2", 100);
        }

        private IEnumerable<Transaction> TestTransactionsWithFee()
        {
            yield return new Transaction(new DateTime(2019, 05, 01), "merchant1", 100) { Fee = 20 };
            yield return new Transaction(new DateTime(2019, 06, 01), "merchant1", 100) { Fee = 20 };
            yield return new Transaction(new DateTime(2019, 06, 06), "merchant1", 100) { Fee = 20 };
            yield return new Transaction(new DateTime(2019, 06, 01), "merchant2", 100) { Fee = 20 };
            yield return new Transaction(new DateTime(2019, 06, 06), "merchant2", 100) { Fee = 20 };
        }

        [TestMethod]
        public void FeeFirstMonthApplied_Success()
        {
            var provider = new TestTransactionProvider(TestTransactionsWithFee);
            var consumer = new TestTransactionConsumer();

            var manager = new TransactionManager(provider, consumer);

            manager.AddRule(new InvoiceFixedFeeRule(20));

            manager.ProcessTransactions();

            var fees = consumer.Transactions.Select(t => t.Fee).ToList();

            CollectionAssert.AreEqual(new List<decimal> { 40m, 40m, 20m, 40m, 20m }, fees);
        }

        [TestMethod]
        public void FeeFirstMonthApplied0Fee_Success()
        {
            var provider = new TestTransactionProvider(TestTransactionsWithoutFee);
            var consumer = new TestTransactionConsumer();

            var manager = new TransactionManager(provider, consumer);

            manager.AddRule(new InvoiceFixedFeeRule(20));

            manager.ProcessTransactions();

            Assert.IsTrue(consumer.Transactions.TrueForAll(t => t.Fee == 0m));
        }
    }
}
