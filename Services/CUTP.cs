using System.Text.RegularExpressions;
using CommandHandling;
using ConfigHandling;
using LogHandling;
using NodaTime;
using Timer = System.Timers.Timer;

namespace Services
{
    public class CUTP
    {
        private readonly Timer _timer;

        public CUTP()
        {
            _timer = new Timer(2000) { AutoReset = true };
            _timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
        }

        public void Start()
        {
            if (
                LogFile.LogReader().Length > 0
                && !Regex.IsMatch(DateTime.Now.ToString("d"), LogFile.Date())
            )
            {
                LogFile.LogCleaner();
            }
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
