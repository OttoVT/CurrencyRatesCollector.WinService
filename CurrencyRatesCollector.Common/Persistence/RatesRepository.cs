using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyRatesCollector.Common.Domain;
using CurrencyRatesCollector.Common.Persistence.DbContext;
using CurrencyRatesCollector.Common.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CurrencyRatesCollector.Common.Persistence
{
    internal sealed class RatesRepository : IRatesRepository
    {
        private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;

        public RatesRepository(DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }
        
        public async Task AddOrIgnoreAsync(IReadOnlyCollection<Rate> rates)
        {
            var entities = rates.Select(x => new RateEntity()
            {
                Currency = x.Currency,
                CreatedAt = x.CreatedAt,
                BaseCurrency = x.BaseCurrency,
                Value = x.Value
            });

            using (var context = new DatabaseContext(_dbContextOptionsBuilder.Options))
            {
                try
                {
                    context.Rates.AddRange(entities);

                    await context.SaveChangesAsync();
                }
                catch (DbUpdateException e) when (e.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    //skip
                }
            }
        }
    }
}
