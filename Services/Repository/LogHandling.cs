namespace LogHandling
{
    public static class LogFile
    {
        public static string LogFilePath { get; } = "./Log.cutp";

        public static string[] LogReader()
        {
            return File.ReadAllLines(LogFilePath);
        }
    }
}
