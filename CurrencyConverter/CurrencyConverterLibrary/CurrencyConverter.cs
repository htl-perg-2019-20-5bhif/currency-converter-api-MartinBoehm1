using System;
using System.Collections.Generic;

namespace CurrencyConverterLibrary
{
    public class CurrencyConverter
    {
        public List<Product> products;
        public List<Rate> rates;
        public CurrencyConverter(string[] rate, string[] product)
        {
            rates = new List<Rate>();
            products = new List<Product>();
            rates.Add(new Rate("EUR", 1));
            for (int i = 1; i < rate.Length; i++)
            {
                rates.Add(new Rate(rate[i].Split(',')[0], decimal.Parse(rate[i].Split(',')[1])));
            }
            for (int i = 1; i < product.Length; i++)
            {
                products.Add(new Product(product[i].Split(',')[0], decimal.Parse(product[i].Split(',')[2])/convert(product[i].Split(',')[1], 1)));
            }
        }
        public decimal convert(string currency, decimal value)
        {
            for(int i = 0; i < rates.Count; i++)
            {
                if (rates[i].currency.Equals(currency))
                {
                    return value * rates[i].rate;
                }
            }
            //should never happen
            return value;
        }
        public List<Product> getInCurr(string curr)
        {
            List<Product> p = new List<Product>();
            for(int i = 0; i < products.Count; i++)
            {
                p.Add(new Product(products[i].name, convert(curr, products[i].costEUR)));
            }
            return p;
        }
    }
}
