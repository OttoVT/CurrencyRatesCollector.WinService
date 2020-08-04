To run on windows perform next commands:

docker run -p 65432:5432  -e PGDATA=/var/lib/postgresql/data/pgdata -v D:\Git\CurrencyRatesCollector\CurrencyRateCollector.DB:/var/lib/postgresql/data -e POSTGRES_DB=rate_collector_db -e POSTGRES_USER=rate_collector_user -e POSTGRES_PASSWORD=123456 --name rate.collector.postgres.local postgres:10.11

cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319

InstallUtil.exe D:\Git\CurrencyRatesCollector\CurrentVersion\CurrencyRatesCollector.WinService.exe

-------------------------

Connection String = Server=127.0.0.1;Port=65432;Database=rate_collector_db;User Id=rate_collector_user;Password=123456;
ENV variable CURRENCY_RATES_COLLECTOR_POSTGRE_SQL_CONNECTION_STRING should be equal to the Connection String above.