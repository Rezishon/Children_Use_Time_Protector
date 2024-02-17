using System.Text.RegularExpressions;

namespace LogHandling
{
    public static class LogFile
    {
        public static string LogFilePath { get; } = "./Log.cutp";

        public static string[] LogReader()
        {
            return File.ReadAllLines(LogFilePath);
        }

        public static void LogCleaner()
        {
            File.WriteAllText(LogFilePath, "");
        }

        public static string? Date()
        {
            // This method should consider different allowed day time
            return Regex.Match(LogReader()[0], @"^(\d*/*)*(?=\s)").ToString();
        }
    }
}
