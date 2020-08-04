using CurrencyRatesCollector.Common.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyRatesCollector.Common.Persistence.DbContext
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public const string SchemaName = "currency_rates_collector";
        public const string MigrationHistoryTable = "__EFMigrationsHistory";

        public DatabaseContext(DbContextOptions<DatabaseContext> options) :
            base(options)
        {
        }

        // Entities
        public DbSet<RateEntity> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);

            BuildRates(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void BuildRates(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RateEntity>()
                .ToTable("currency_rates")
                .HasKey(x => new { x.CreatedAt, x.Currency });

            modelBuilder.Entity<RateEntity>()
                .HasIndex(x => x.BaseCurrency);

            modelBuilder.Entity<RateEntity>()
                .HasIndex(x => x.Currency);

            modelBuilder.Entity<RateEntity>()
                .HasIndex(x => x.CreatedAt);
        }
    }
}
