using System;
using System.Threading.Tasks;
using CurrencyRatesCollector.Common.Domain;
using CurrencyRatesCollector.Common.Persistence;
using CurrencyRatesCollector.Common.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyRatesCollector.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var alphaAvantageApiKey = Environment.GetEnvironmentVariable("ALPHA_VANTAGE_API_KEY");
            //var downloader = new AlphaVantageDownloader(alphaAvantageApiKey);

            //var x = downloader.DownloadRates().GetAwaiter().GetResult();
            var serviceCollection = new ServiceCollection();
            var connString = Environment.GetEnvironmentVariable("CURRENCY_RATES_COLLECTOR_POSTGRE_SQL_CONNECTION_STRING");
            serviceCollection.AddPersistence(connString);
            serviceCollection.AddSingleton<EcbRateDownloader>();
            var sp = serviceCollection.BuildServiceProvider();

            //var contextOptions = sp.GetRequiredService<DbContextOptionsBuilder<DatabaseContext>>();
            //using (var context = new DatabaseContext(contextOptions.Options))
            //{
            //    context.Database.Migrate();
            //}

            var rates = sp.GetRequiredService<EcbRateDownloader>().DownloadRates().GetAwaiter().GetResult();
            sp.GetRequiredService<IRatesRepository>().AddOrIgnoreAsync(rates).GetAwaiter().GetResult();
        }
    }
}
