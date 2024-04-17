using System.Diagnostics;
using System.Text.RegularExpressions;
using Children_Use_Time_Protector.Repository.ConfigHandling;
using Children_Use_Time_Protector.Repository.LogHandling;
using Children_Use_Time_Protector.Repository.TimeHandling;
using Microsoft.Toolkit.Uwp.Notifications;
using NodaTime;
using Timer = System.Timers.Timer;

namespace Children_Use_Time_Protector.Service
{
    public class CUTP
    {
        private readonly Timer _timer;

        public CUTP()
        {
            _timer = new Timer(1000) { AutoReset = true };
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
                if (Time.Allowed() == false)
                {
                    Console.WriteLine("Time has ended");
                    Stop();
                }
                else if (Time.Allowed() == null)
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
            var processInfo = new ProcessStartInfo
            {
                Verb = "runas", // Run as administrator
                // LoadUserProfile = true,
                FileName = "powershell.exe",
                Arguments = @"S:\Projects\Children_Use_Time_Protector\FinalConsole\bin\Debug\net8.0-windows10.0.17763.0\FinalConsole.exe",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
           Process.Start(processInfo);

        }
    }
}
