using System;
using System.Configuration;
using TransactionFeeCalculator.Rules;

namespace TransactionFeeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string transactionsFileName = ConfigurationManager.AppSettings["TransactionsFilePath"];

            var provider = new FileTransactionProvider(transactionsFileName);
            var consumer = new ConsoleTransactionConsumer();

            TransactionManager manager = new TransactionManager(provider, consumer);

            InitializeRules(manager);

            try
            {
                manager.ProcessTransactions();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }

        private static void InitializeRules(TransactionManager manager)
        {
            manager.AddRule(new FeePercentageRule(1))
                   .ThenAddRule(new FeePercentageDiscountRule("TELIA", 10))
                   .ThenAddRule(new FeePercentageDiscountRule("CIRCLE_K", 20))
                   .ThenAddRule(new InvoiceFixedFeeRule(29));
        }
    }
}
