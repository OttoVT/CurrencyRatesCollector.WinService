using System;

namespace CurrencyRatesCollector.Common.Persistence.Entities
{
    public class RateEntity
    {
        public string BaseCurrency { get; set; }
        public string Currency { get; set; }
        public decimal Value { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
