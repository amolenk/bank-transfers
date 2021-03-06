﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace BankTransactionParser
{
    public class RabobankTransactionReader : ITransactionReader
    {
        private readonly CsvReader _reader;

        public RabobankTransactionReader(TextReader reader)
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
    }
}
