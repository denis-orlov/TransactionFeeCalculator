using System;
using System.Collections.Generic;
using System.IO;

namespace TransactionFeeCalculator
{
    public class FileTransactionProvider : ITransactionProvider
    {
        private string fileName;

        public FileTransactionProvider(string fileName)
        {
            this.fileName = fileName;
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException($"File {fileName} not found!");
            }

            var lines = File.ReadLines(fileName);
            int i = 0;

            foreach (string line in lines)
            {
                i++;

                if (string.IsNullOrEmpty(line.Trim()))
                {
                    continue;
                }

                Transaction transaction = null;

                try
                {
                    transaction = Transaction.Parse(line);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line number {i}:");
                    Console.WriteLine(ex);
                    continue;
                }

                yield return transaction;
            }
        }
    }
}
