using System;
using System.Collections.Generic;
using System.Text;

namespace BankTransactionParser
{
    public class Transaction
    {
        public string AccountNr { get; set; }

        public DateTime Date { get; set; }

        public Decimal Amount { get; set; }

        public string Payee { get; set; }

        public string Memo { get; set; }

        public string Category { get; set; }
    }
}
