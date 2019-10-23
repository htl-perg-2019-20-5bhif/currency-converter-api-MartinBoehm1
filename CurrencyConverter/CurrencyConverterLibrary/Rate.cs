using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverterLibrary
{
    public class Rate
    {
        public string currency;
        public decimal rate;
        public Rate(string currency, decimal rate)
        {
            this.currency = currency;
            this.rate = rate;
        }
    }
}
