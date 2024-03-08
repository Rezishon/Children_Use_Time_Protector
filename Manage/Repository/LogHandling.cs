using System.Text.RegularExpressions;
using CommandHandling;

namespace LogHandling
{
    public class LogFile
    {
        /// <summary>
        /// It find application path and then custom the path string
        /// </summary>
        public static string LogFilePath { get; } =
            Regex.Replace(
                Commands.Pwd(),
                @"\\Children_Use_Time_Protector\\(\w*\W*)*$",
                @"\Children_Use_Time_Protector\Manage\bin\Debug\net8.0\Log.cutp"
            );

        /// <summary>
        /// Read log file content
        /// </summary>
        /// <returns>
        ///     <list type="bullet|number|table">
        ///         <item>
        ///             <term>Array of string</term>
        ///             <description>Each index contains content of each line in log file</description>
        ///         </item>
        ///     </list>
        /// </returns>
        public static string[] LogReader()
        {
            return File.ReadAllLines(LogFilePath);
        }

        /// <summary>
        /// Clear file content
        /// </summary>
        public static void LogCleaner()
        {
            File.WriteAllText(LogFilePath, "");
        }

        public static string Date(
        /// <summary>
        /// Return date part of string<br/>
        /// Optionally, this method can find date of income string or other patterns
        /// </summary>
        /// <param name="inputString">String which you want to find date from it</param>
        /// <param name="pattern">Pattern to find things more than date, like time</param>
        /// <returns>
        ///     <term>String</term>
        ///     <description>Found pattern | By default it's date string | Can be null</description>
        /// </returns>
        /// <remarks>This method is a multifunction method</remarks>
            string? inputString = null,
            string pattern = @"^\d{4}(-\d{2})+(?=T)"
        )
        {
            // By default return date of first line of log file
            if (inputString == null)
            {
                // This method should consider different allowed day time
                return Regex.Match(LogReader()[0], pattern).ToString();
            }
            // Return date of income string in NodaTime library format
            else
            {
                return Regex.Match(inputString, pattern).ToString();
            }
        }

        public static string Time(string? inputTime = null)
        /// <summary>
        /// Return time part of string<br/>
        /// Optionally, this method can find time of income string
        /// </summary>
        /// <param name="inputTime">String which you want to find time from it</param>
        /// <returns>
        ///     <term>String</term>
        ///     <description>Found pattern | Can be null</description>
        /// </returns>
        /// <remarks>By default it find first log line time value</remarks>
        {
            return Date(inputTime, @"(?<=T)\d{2}:\d{2}:\d{2}(?=\s)");
        }
    }
}
