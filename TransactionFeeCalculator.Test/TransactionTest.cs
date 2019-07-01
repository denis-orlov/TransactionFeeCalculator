using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TransactionFeeCalculator.Test
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void ParseTransaction_Success()
        {
            string line = "2009-11-03 TEST 23.56";
            var transaction = Transaction.Parse(line);
            Assert.AreEqual(new DateTime(2009, 11, 03), transaction.Date);
            Assert.AreEqual("TEST", transaction.MerchantName);
            Assert.AreEqual(23.56m, transaction.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid values amount!")]
        public void ParseTransaction_WrongArguments_Failure()
        {
            string line = "2009-11-03 TEST 100 qqq www";
            var transaction = Transaction.Parse(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid values amount!")]
        public void ParseTransaction_OneArgument_Failure()
        {
            string line = "2009-56-03";
            var transaction = Transaction.Parse(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid date format!")]
        public void ParseTransaction_WrongDate_Failure()
        {
            string line = "2009-56-03 TEST 23.56";
            var transaction = Transaction.Parse(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid amount format!")]
        public void ParseTransaction_WrongAmount_Failure()
        {
            string line = "2009-11-03 TEST 23.56a";
            var transaction = Transaction.Parse(line);
        }

        [TestMethod]
        public void ToString_Success()
        {
            Transaction tran = new Transaction(new DateTime(2019, 05, 01), "merchant1", 100);
            tran.Fee = 2;

            Assert.AreEqual("2019-05-01 merchant1 2.00", tran.ToString());
        }
    }
}
