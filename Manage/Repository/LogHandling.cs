using System.Text.RegularExpressions;
using CommandHandling;

namespace LogHandling
{
    public class LogFile
    {
        // Log file Path property
        public static string LogFilePath { get; } =
            Regex.Replace(
                Commands.Pwd(),
                @"\\Children_Use_Time_Protector\\(\w*\W*)*$",
                @"\Children_Use_Time_Protector\Manage\bin\Debug\net8.0\Log.cutp"
            );

        // Read log file content
        public static string[] LogReader()
        {
            return File.ReadAllLines(LogFilePath);
        }

        // Clear file content
        public static void LogCleaner()
        {
            File.WriteAllText(LogFilePath, "");
        }

        public static string Date(string inputDate = "")
        // Return date part of string
        {
            if (inputDate.Equals(""))
            // By default return date of first line of log file
            {
                // This method should consider different allowed day time
                return Regex.Match(LogReader()[0], @"^\d{4}(-\d{2})+(?=T)").ToString();
            }
            // Return date of income string in NodaTime library format
            else
            {
                return Regex.Match(inputDate, @"^\d{4}(-\d{2})+(?=T)").ToString();
            }
        }

        // Return time part of string
        // (?<=T)\d{2}:\d{2}:\d{2}(?=\s) time regex
    }
}
