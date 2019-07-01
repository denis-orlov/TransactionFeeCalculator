namespace TransactionFeeCalculator
{
    public interface ITransactionConsumer
    {
        void ConsumeTransaction(Transaction transaction);
    }
}
