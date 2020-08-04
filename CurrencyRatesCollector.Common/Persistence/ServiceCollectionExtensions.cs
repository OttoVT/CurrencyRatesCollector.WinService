using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyRatesCollector.Common.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyRatesCollector.Common.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRatesRepository, RatesRepository>();

            services.AddSingleton<DbContextOptionsBuilder<DatabaseContext>>(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                optionsBuilder.UseNpgsql(connectionString,
                    builder =>
                        builder.MigrationsHistoryTable(
                            DatabaseContext.MigrationHistoryTable,
                            DatabaseContext.SchemaName));

                return optionsBuilder;
            });

            return services;
        }
    }
}
