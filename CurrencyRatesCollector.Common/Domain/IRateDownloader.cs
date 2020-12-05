using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaVantage.Net.Stocks;
using AlphaVantage.Net.Stocks.TimeSeries;

namespace CurrencyRatesCollector.Common.Domain
{
    public interface IRateDownloader
    {
        Task<IReadOnlyCollection<Rate>> DownloadRates();
    }

    public class AlphaVantageDownloader : IRateDownloader
    {
        private readonly AlphaVantageStocksClient _client;
        private readonly List<string> _symbols;

        public AlphaVantageDownloader(string apiKey)
        {
            _symbols = new List<string>()
            {
                "AAPL",
            };
            _client = new AlphaVantageStocksClient(apiKey);
        }

        public async Task<IReadOnlyCollection<Rate>> DownloadRates()
        {
            //_symbols.;
            var timeSeries = await _client.RequestIntradayTimeSeriesAsync("AAPL", IntradayInterval.ThirtyMin, TimeSeriesSize.Compact);

            //var dateTime
            var dataPoint = timeSeries?.DataPoints?.FirstOrDefault();

            if (dataPoint == null)
                return Array.Empty<Rate>();

            var rate = new Rate("USD", "AAPL", dataPoint.ClosingPrice, dataPoint.Time) { };
            return new List<Rate>()
                {
                    rate
                };

        }
    }
}
