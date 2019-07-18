using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace BankTransactionParser
{
    public interface ITransactionReader : IDisposable
    {
        Transaction ReadTransaction();
    }
}
