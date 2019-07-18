using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankTransactionParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var transactions = LoadTransactions(args[0]);

            SaveTransactions(
                Path.Combine(Path.GetDirectoryName(args[0]), "transactions.csv"),
                transactions);
        }

        private static IEnumerable<Transaction> LoadTransactions(string path)
        {
            var fileName = Path.GetFileName(path);
            
            ITransactionReader reader = null;

            try
            { 
                if (fileName.StartsWith("CSV_O_"))
                {
                    Console.WriteLine("Detected Rabobank file.");
                    reader = new RabobankTransactionReader(File.OpenText(path));
                }
                else
                {
                    Console.WriteLine("Detected ING file.");
                    reader = new INGTransactionReader(File.OpenText(path));
                }

                Transaction transaction;
                while ((transaction = reader.ReadTransaction()) != null)
                {
                    yield return transaction;
                }
            }
            finally
            {
                reader?.Dispose();
            }
        }

        private static void SaveTransactions(string path, IEnumerable<Transaction> transactions)
        {
            using (var writer = new BudgetTransactionWriter(File.CreateText(path)))
            {
                foreach (var transaction in transactions)
                {
                    writer.WriteTransaction(transaction);
                }
            }
        }
    }
}
