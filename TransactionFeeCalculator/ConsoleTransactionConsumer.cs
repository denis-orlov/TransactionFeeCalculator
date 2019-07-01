using System;

namespace TransactionFeeCalculator
{
    public class ConsoleTransactionConsumer : ITransactionConsumer
    {
        public void ConsumeTransaction(Transaction tran)
        {
            Console.WriteLine(tran.ToString());
        }
    }
}
