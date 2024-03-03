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
    }
}
