using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CurrencyRatesCollector.Common.Domain
{
    public class EcbRateDownloader
    {
        private static readonly char[] splitter = new[] {'-'};
        public EcbRateDownloader()
        {
        }


        public IReadOnlyCollection<Rate> DownloadRates()
        {
            var doc = new XmlDocument();
            doc.Load(@"http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");

            XmlNodeList time = doc.SelectNodes("//*[@time]");

            var splittedDate = time[0].Attributes["time"].Value.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            var year = int.Parse(splittedDate[0]);
            var month = int.Parse(splittedDate[1]);
            var day = int.Parse(splittedDate[2]);
            var dateTime = new DateTime(year, month, day, 0, 0, 0, 0, DateTimeKind.Utc);
            var rates = new List<Rate>(40);
            rates.Add(new Rate("EUR", "EUR", 1, dateTime));

            XmlNodeList nodes = doc.SelectNodes("//*[@currency]");

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    if (node == null)
                        continue;

                    var rate = new Rate
                        ("EUR",
                        node.Attributes["currency"].Value,
                        Decimal.Parse(node.Attributes["rate"].Value, NumberStyles.Any, new CultureInfo("en-Us")),
                        dateTime);
                    rates.Add(rate);
                }
            }

            return rates;
        }
    }
}
