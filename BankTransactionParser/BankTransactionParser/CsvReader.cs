using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace BankTransactionParser
{
    public class CsvReader : IDisposable
    {
        private readonly TextReader _reader;

        public CsvReader(TextReader reader)
        {
            _reader = reader;

            // Read header
            _reader.ReadLine();
        }

        public IList<string> ReadLine()
        {
            var line = _reader.ReadLine();
            if (line != null)
            {
                return ExtractColumns(line);
            }

            return null;
        }

        public void Dispose()
        {
            _reader.Dispose();
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
