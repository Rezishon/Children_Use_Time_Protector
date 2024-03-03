using System.Text.RegularExpressions;
using CommandHandling;

namespace LogHandling
{
    public static class LogFile
    {
        public static string LogFilePath { get; } =
            Regex.Replace(
                Commands.Pwd(),
                @"\\Children_Use_Time_Protector\\(\w*\W*)*$",
                @"\Children_Use_Time_Protector\Manage\bin\Debug\net8.0\Log.cutp"
            );

        public static string[] LogReader()
        {
            return File.ReadAllLines(LogFilePath);
        }

        public static void LogCleaner()
        {
            File.WriteAllText(LogFilePath, "");
        }

        public static string Date(string inputDate = "")
        {
            if (inputDate.Equals(""))
            {
                // This method should consider different allowed day time
                return Regex.Match(LogReader()[0], @"^\d{4}(-\d{2})+(?=T)").ToString();
            }
            else
            {
                return Regex.Match(inputDate, @"^\d{4}(-\d{2})+(?=T)").ToString();
            }
        }
    }
}
