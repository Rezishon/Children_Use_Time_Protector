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
                string[] lines = new string[]
                {
                    SystemClock
                        .Instance.GetCurrentInstant()
                        .InZone(DateTimeZoneProviders.Tzdb.GetSystemDefault())
                        .ToString()
                };
                File.AppendAllLines(LogFile.LogFilePath, lines);
                if (TimeHandling.Time.Allowed() == false)
                {
                    Console.WriteLine("Time has ended");
                    Stop();
                }
                else if (TimeHandling.Time.Allowed() == null)
                {
                    Console.WriteLine("10 minute remained");
                }
                else
                {
                    Console.WriteLine("Still has time");
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
        }

        public void Start()
        {
            // Check user has time to start or not
            if (
                LogFile.LogReader().Length > 0
                && !Regex.IsMatch(
                    LogFile.Date(
                        SystemClock
                            .Instance.GetCurrentInstant()
                            .InZone(DateTimeZoneProviders.Tzdb.GetSystemDefault())
                            .ToString()
                    ),
                    LogFile.Date()
                )
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
