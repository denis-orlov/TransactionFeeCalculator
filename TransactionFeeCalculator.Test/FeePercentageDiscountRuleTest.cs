using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TransactionFeeCalculator.Rules;

namespace TransactionFeeCalculator.Test
{
    [TestClass]
    public class FeePercentageDiscountRuleTest
    {
        [TestMethod]
        public void FeeDiscount10PercentApplied_Success()
        {
            Transaction transaction = new Transaction(DateTime.Today, "merchant1", 100);
            transaction.Fee = 1;
            FeePercentageDiscountRule rule = new FeePercentageDiscountRule("merchant1", 10);

            rule.Apply(transaction);

            Assert.AreEqual(transaction.Fee, 0.9m);
        }

        [TestMethod]
        public void FeeDiscountWrongMerchantApplied_Failure()
        {
            Transaction transaction = new Transaction(DateTime.Today, "merchant1", 100);
            transaction.Fee = 1;
            FeePercentageDiscountRule rule = new FeePercentageDiscountRule("merchant2", 10);

            rule.Apply(transaction);

            Assert.AreEqual(transaction.Fee, 1m);
        }
    }
}
