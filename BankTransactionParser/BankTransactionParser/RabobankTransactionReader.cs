using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace BankTransactionParser
{
    public class RabobankTransactionReader : IDisposable
    {
        private readonly TextReader _reader;

        public RabobankTransactionReader(TextReader reader)
        {
            _reader = reader;

            // Read header
            _reader.ReadLine();
        }

        public Transaction ReadTransaction()
        {
            var line = _reader.ReadLine();
            if (line != null)
            {
                var columns = ExtractColumns(line);

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
            var result = new Transaction
            {
                AccountNr = columns[0],
                Date = DateTime.ParseExact(columns[4], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Amount = Convert.ToDecimal(columns[6], CultureInfo.GetCultureInfo("NL")),
                Payee = columns[9],
                Memo = columns[19]
            };

            return result;
        }

        private static IList<string> ExtractColumns(string line)
        {
            List<string> list = new List<string>();
            using (StringReader stringReader = new StringReader(line))
            {
                string str;
                while ((str = ReadValue((TextReader)stringReader)) != null)
                    list.Add(str);
            }
            return list;
        }

        private static string ReadValue(TextReader reader)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (reader.Peek() == -1)
                return (string)null;
            if (reader.Peek() == 34)
            {
                reader.Read();
                while (reader.Peek() != 34)
                    stringBuilder.Append((char)reader.Read());
                reader.Read();
            }
            else
            {
                while (reader.Peek() != -1 && reader.Peek() != 44)
                    stringBuilder.Append((char)reader.Read());
            }
            if (reader.Peek() == 44)
                reader.Read();
            return ((object)stringBuilder).ToString();
        }
    }
}
