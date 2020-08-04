using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyRatesCollector.Common.Domain;

namespace CurrencyRatesCollector.Common.Persistence
{
    public interface IRatesRepository
    {
        Task AddOrIgnoreAsync(IReadOnlyCollection<Rate> rates);
    }
}
