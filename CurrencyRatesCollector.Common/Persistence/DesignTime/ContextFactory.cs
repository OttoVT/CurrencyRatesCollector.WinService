using System;
using CurrencyRatesCollector.Common.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CurrencyRatesCollector.Common.Persistence.DesignTime
{
    public class ContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var connString = Environment.GetEnvironmentVariable("CURRENCY_RATES_COLLECTOR_POSTGRE_SQL_CONNECTION_STRING");

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseNpgsql(connString);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
