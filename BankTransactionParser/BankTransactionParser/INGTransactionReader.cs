using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace BankTransactionParser
{
    public class INGTransactionReader : ITransactionReader
    {
        private readonly CsvReader _reader;

        public INGTransactionReader(TextReader reader)
        {
            _reader = new CsvReader(reader);
        }

        public Transaction ReadTransaction()
        {
            var columns = _reader.ReadLine();
            if (columns != null)
            {
                return CreateTransaction(columns);
            }

            return null;
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

        private Transaction CreateTransaction(IList<string> columns)
        {
            var amount = Convert.ToDecimal(columns[6], CultureInfo.GetCultureInfo("NL"));
            if (string.Equals(columns[5], "af", StringComparison.InvariantCultureIgnoreCase))
            {
                amount *= -1;
            }
            
            var result = new Transaction
            {
                AccountNr = columns[2],
                Date = DateTime.ParseExact(columns[0], "yyyyMMdd", CultureInfo.InvariantCulture),
                Amount = amount,
                Payee = columns[1],
                Memo = columns[8]
            };

            return result;
        }
    }
}
