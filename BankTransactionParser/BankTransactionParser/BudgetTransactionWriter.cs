using System;
using System.Globalization;
using System.IO;

namespace BankTransactionParser
{
    public class BudgetTransactionWriter : IDisposable
    {
        private readonly TextWriter _writer;

        public BudgetTransactionWriter(TextWriter writer)
        {
            _writer = writer;
        }

        public void WriteTransaction(Transaction transaction)
        {
            _writer.WriteLine(
                "\"{0}\";{1};\"{2}\";\"{3}\";\"{4}\";{5}",
                transaction.AccountNr,
                transaction.Date.ToString(CultureInfo.GetCultureInfo("nl-NL")),
                transaction.Payee,
                transaction.Category,
                transaction.Memo,
                transaction.Amount.ToString(CultureInfo.GetCultureInfo("nl-NL")));
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
