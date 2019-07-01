namespace TransactionFeeCalculator.Rules
{
    public class FeePercentageRule : ITransactionRule
    {
        public decimal FeePercent { get; set; }

        public FeePercentageRule(decimal feePercent)
        {
            FeePercent = feePercent;
        }

        public void Apply(Transaction transaction)
        {
            transaction.Fee += transaction.Amount / 100 * FeePercent;
        }
    }
}
