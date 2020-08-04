using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRatesCollector.Common.Domain;
using CurrencyRatesCollector.Common.Persistence;
using CurrencyRatesCollector.Common.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyRatesCollector.WinService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var serviceCollection = new ServiceCollection();
            var connString = Environment.GetEnvironmentVariable("CURRENCY_RATES_COLLECTOR_POSTGRE_SQL_CONNECTION_STRING");
            serviceCollection.AddPersistence(connString);
            serviceCollection.AddSingleton<EcbRateDownloader>();
            var sp = serviceCollection.BuildServiceProvider();

            var contextOptions = sp.GetRequiredService<DbContextOptionsBuilder<DatabaseContext>>();
            using (var context = new DatabaseContext(contextOptions.Options))
            {
                context.Database.Migrate();
            }

            var ecbService = new EcbRateCollectorService(
                sp.GetRequiredService<EcbRateDownloader>(),
                sp.GetRequiredService<IRatesRepository>());

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                ecbService
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
