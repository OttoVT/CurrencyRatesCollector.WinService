using System;

namespace CurrencyRatesCollector.Common.Domain
{
    public class Rate
    {
        public Rate(string baseCurrency, string currency, decimal value, DateTime createdAt)
        {
            BaseCurrency = baseCurrency;
            Currency = currency;
            Value = value;
            CreatedAt = createdAt.Date;
        }

        public string BaseCurrency { get; }
        public string Currency { get; }
        public decimal Value { get; }
        public DateTime CreatedAt { get; }
    }
}