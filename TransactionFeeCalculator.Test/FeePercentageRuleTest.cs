using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TransactionFeeCalculator.Rules;

namespace TransactionFeeCalculator.Test
{
    [TestClass]
    public class FeePercentageRuleTest
    {
        [TestMethod]
        public void Fee2PercentApplied_Success()
        {
            Transaction transaction = new Transaction(DateTime.Today, "",  100);
            FeePercentageRule rule = new FeePercentageRule(2);

            rule.Apply(transaction);

            Assert.AreEqual(transaction.Fee, 2m);
        }
    }
}
