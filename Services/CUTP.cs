using Timer = System.Timers.Timer;

namespace LogHandling
{
    public class CUTP
    {
        private readonly Timer _timer;

        public CUTP()
        {
            _timer = new Timer(600000) { AutoReset = true };
            _timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            string[] lines = new string[] { LogFile.LogContent };
            File.AppendAllLines(LogFile.LogFilePath, lines);
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
