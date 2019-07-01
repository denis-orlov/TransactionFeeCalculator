namespace TransactionFeeCalculator.Rules
{
    public interface ITransactionRule
    {
        void Apply(Transaction transaction);
    }
}
