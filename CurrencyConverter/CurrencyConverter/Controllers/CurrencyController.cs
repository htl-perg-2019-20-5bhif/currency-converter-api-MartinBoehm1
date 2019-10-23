using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CurrencyConverterLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {

        String[] rate = System.IO.File.ReadAllLines("Files/Rates.csv");
        String[] products = System.IO.File.ReadAllLines("Files/Products.csv");
        CurrencyConverter c;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger)
        {
            
            _logger = logger;
        }
        [HttpGet]
        [Route("products/{product}/price")]
        public string GetSingle([FromRoute]string product, [FromQuery]string targetCurrency)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://cddataexchange.blob.core.windows.net/data-exchange/htl-homework/ExchangeRates.csv", "Files/Rates.csv");
                client.DownloadFile("https://cddataexchange.blob.core.windows.net/data-exchange/htl-homework/Prices.csv", "Files/Products.csv");
            }
            string[] rate = System.IO.File.ReadAllLines("Files/Rates.csv");
            string[] products = System.IO.File.ReadAllLines("Files/Products.csv");
            c = new CurrencyConverter(rate, products);
            List<Product> p = c.getInCurr("EUR");
            /*Console.WriteLine("asdfasdfasdf");
            Console.WriteLine(p.Count.ToString());*/
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].name.Equals(product))
                {
                    return "{ \"price\": "+Math.Round(c.convert(targetCurrency, p[i].costEUR),2)+" }";
                }
            }
            return null;
            /*var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();*/
        }
        [HttpGet]
        [Route("products")]
        public List<Product> Get()
        {
            c = new CurrencyConverter(rate, products);
            List<Product> p=c.getInCurr("EUR");
            /*Console.WriteLine("asdfasdfasdf");
            Console.WriteLine(p.Count.ToString());
            for (int i = 0; i < p.Count; i++)
            {
                Console.WriteLine(p[i]);
            }*/
            return p;
            /*var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();*/
        }
    }
}
