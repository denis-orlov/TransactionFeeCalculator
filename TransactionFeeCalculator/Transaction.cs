using System;
using System.Globalization;

namespace TransactionFeeCalculator
{
    public class Transaction
    {
        public DateTime Date { get; private set; }
        public string MerchantName { get; private set; }
        public decimal Amount { get; private set; }
        public decimal Fee { get; set; }

        public Transaction(DateTime date, string merchantName, decimal amount)
        {
            Date = date;
            MerchantName = merchantName;
            Amount = amount;
        }

        /// <summary>
        /// Helper method that converts string to Transaction object.
        /// </summary>
        /// <param name="line">String containing transaction values.</param>
        /// <returns>Parsed transaction object.</returns>
        public static Transaction Parse(string line)
        {
            var values = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length != 3)
            {
                throw new ArgumentException("Invalid values amount!");
            }

            DateTime date;
            string merchantName = values[1];
            decimal amount;

            try
            {
                date = DateTime.Parse(values[0]);
            }
            catch (FormatException fe)
            {
                throw new ArgumentException("Invalid date format!", fe);
            }

            try
            {
                amount = Decimal.Parse(values[2], CultureInfo.InvariantCulture);
            }
            catch (FormatException fe)
            {
                throw new ArgumentException("Invalid amount format!", fe);
            }

            return new Transaction(date, merchantName, amount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Transaction string representation with calculated fee.</returns>
        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd} {MerchantName} {Fee.ToString("0.00", CultureInfo.InvariantCulture)}";
        }
    }
}
