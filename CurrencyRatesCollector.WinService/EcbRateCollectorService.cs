using CurrencyRatesCollector.Common.Domain;
using CurrencyRatesCollector.Common.Persistence;
using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace CurrencyRatesCollector.WinService
{
    public partial class EcbRateCollectorService : ServiceBase
    {
        private readonly EcbRateDownloader _ecbRateDownloader;
        private readonly IRatesRepository _ratesRepository;
        Timer timer = new Timer(); // name space(using System.Timers;)  
        public EcbRateCollectorService(
            EcbRateDownloader ecbRateDownloader,
            IRatesRepository ratesRepository)
        {
            _ecbRateDownloader = ecbRateDownloader;
            _ratesRepository = ratesRepository;
            InitializeComponent();
        }

        public void CallOnStart()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 3 * 60 * 60 * 1_000; //number in milliseconds  
            timer.Enabled = true;
        }
        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }
        public void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall at " + DateTime.Now);

            try
            {
                var rates = _ecbRateDownloader.DownloadRates().GetAwaiter().GetResult();
                _ratesRepository.AddOrIgnoreAsync(rates).GetAwaiter().GetResult();

                WriteToFile("Service has finished recall at " + DateTime.Now);
            }
            catch (Exception exception)
            {
                WriteToFile("Service has failed at " + DateTime.Now + " with " + exception.StackTrace + " " + exception.Message);
            }
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
