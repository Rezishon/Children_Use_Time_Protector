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
            string[] lines = new string[] { DateTime.Now.ToString("") };
            File.AppendAllLines(LogFile.LogFilePath, lines);
            if (TimeHandling.Time.Allowed() == false)
            {
                System.Console.WriteLine("Time has ended");
                Stop();
            }
            else if (TimeHandling.Time.Allowed() == null)
            {
                System.Console.WriteLine("10 minute remained");
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
