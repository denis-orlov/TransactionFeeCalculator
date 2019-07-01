using System;
using TransactionFeeCalculator.Rules;

namespace TransactionFeeCalculator
{
    public class TransactionManager
    {
        private ITransactionProvider provider;
        private ITransactionConsumer cosnumer;

        private RulesManager rulesManager;

        public TransactionManager(ITransactionProvider provider, ITransactionConsumer cosnumer)
        {
            this.provider = provider;
            this.cosnumer = cosnumer;
        }

        public void ProcessTransactions()
        {
            try
            {
                var transactions = provider.GetTransactions();

                foreach (var tran in transactions)
                {
                    rulesManager.GetRules().ForEach(r => r.Apply(tran));
                    cosnumer.ConsumeTransaction(tran);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing transactions:");
                Console.WriteLine(ex);
            }
        }

        public RulesManager AddRule(ITransactionRule rule)
        {
            rulesManager = new RulesManager();

            rulesManager.ThenAddRule(rule);
            return rulesManager;
        }
    }
}
