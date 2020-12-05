using System;
using CurrencyRatesCollector.Common.Domain;

namespace CurrencyRatesCollector.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var alphaAvantageApiKey = Environment.GetEnvironmentVariable("ALPHA_VANTAGE_API_KEY");
            var downloader = new AlphaVantageDownloader(alphaAvantageApiKey);

            var x = downloader.DownloadRates().GetAwaiter().GetResult();
        }
    }
}
