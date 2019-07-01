using System.Collections.Generic;
using TransactionFeeCalculator.Rules;

namespace TransactionFeeCalculator
{
    public class RulesManager
    {
        private List<ITransactionRule> rules { get; } = new List<ITransactionRule>();

        public RulesManager ThenAddRule(ITransactionRule rule)
        {
            rules.Add(rule);
            return this;
        }

        public List<ITransactionRule> GetRules()
        {
            return rules;
        }
    }
}
