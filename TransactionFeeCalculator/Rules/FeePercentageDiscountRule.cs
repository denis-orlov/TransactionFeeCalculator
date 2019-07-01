namespace TransactionFeeCalculator.Rules
{
    public class FeePercentageDiscountRule : ITransactionRule
    {
        public string MerchantName { get; set; }

        public decimal DiscountPercent { get; set; }

        public FeePercentageDiscountRule(string merchantName, decimal discountPercent)
        {
            MerchantName = merchantName;
            DiscountPercent = discountPercent;
        }

        public void Apply(Transaction transaction)
        {
            if (transaction.MerchantName == MerchantName)
            {
                transaction.Fee -= transaction.Fee / 100 * DiscountPercent; 
            }
        }
    }
}
