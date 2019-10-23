using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverterLibrary
{
    public class Product
    {
        public string name;
        public decimal costEUR;
        public Product(string name, decimal costEUR)
        {
            this.name = name;
            this.costEUR = costEUR;
        }

        public override string ToString()
        {
            return "name: " + name + " cost: " + costEUR;
        }
    }
}
