using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyRatesCollector.Common.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "currency_rates_collector");

            migrationBuilder.CreateTable(
                name: "currency_rates",
                schema: "currency_rates_collector",
                columns: table => new
                {
                    Currency = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    BaseCurrency = table.Column<string>(nullable: true),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currency_rates", x => new { x.CreatedAt, x.Currency });
                });

            migrationBuilder.CreateIndex(
                name: "IX_currency_rates_BaseCurrency",
                schema: "currency_rates_collector",
                table: "currency_rates",
                column: "BaseCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_currency_rates_CreatedAt",
                schema: "currency_rates_collector",
                table: "currency_rates",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_currency_rates_Currency",
                schema: "currency_rates_collector",
                table: "currency_rates",
                column: "Currency");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "currency_rates",
                schema: "currency_rates_collector");
        }
    }
}
