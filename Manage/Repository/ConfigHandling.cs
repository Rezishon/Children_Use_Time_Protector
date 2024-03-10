using System.Text.RegularExpressions;
using CommandHandling;

namespace ConfigHandling
{
    #region Config file class
    /// <summary>
    /// Contains methods which are related to config file itself
    /// </summary>
    public static class ConfigFile
    {
        #region Config file parts dictionary
        /// <summary>
        /// In this dictionary line number of each part with its name stores
        /// </summary>
        public static Dictionary<string, int> ConfigLinesNumberDictionary = new Dictionary<
            string,
            int
        >()
        {
            { "Root", 0 },
            { "Service", 1 }
        };
        #endregion

        #region Config file path
        /// <summary>
        /// Config file path comes from application path with some customization
        /// </summary>
        public static string ConfigFilePath { get; } =
            Regex.Replace(
                Commands.Pwd(),
                @"\\Children_Use_Time_Protector\\(\w*\W*)*$",
                @"\Children_Use_Time_Protector\Manage\bin\Debug\net8.0\Config.cutp"
            );
        #endregion
        private static string NullString { get; } = "*";

        #region Config file builder method
        /// <summary>
        /// Make raw config file
        /// </summary>
        /// <remarks>Used for first run of application</remarks>
        public static void ConfigFileBuilder()
        {
            try
            {
                string RootPart =
                    $"{Hashing.Hash.ToSha256("0")};{NullString};{NullString};{NullString};{NullString};";
                string ServicePart =
                    $"{Hashing.Hash.ToSha256("0")};{NullString};{NullString};{NullString};{NullString};{NullString};{NullString};{NullString};{NullString};";

                File.WriteAllText(ConfigFilePath, $"{RootPart}\n{ServicePart}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        #endregion

        #region Config file reader method
        /// <summary>
        /// Read all config file content
        /// </summary>
        /// <returns>
        ///     <term>String array</term>
        ///     <description>Contents return line by line with their line number as index of array</description>
        /// </returns>
        public static string[] ConfigFileReader()
        {
            return File.ReadAllLines(ConfigFilePath);
        }
        #endregion
    }
    #endregion
    #region Root part of config file class
    public static class RootPart
    {
        #region Reading Config file
        #endregion
        #region Config file parts dictionary
        /// <summary>
        /// Main parts of root part in config file with their index
        /// </summary>
        #endregion
        #region Status reader method
        /// <summary>
        /// Status of root from root part of config file
        /// </summary>
        /// <returns>
        ///     <term>Boolean</term>
        ///     <description>True for available root password | False for not available root password</description>
        /// </returns>
        #endregion

        #region Root main password reader method
        /// <summary>
        /// Read root main password from root part of config file
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>In SHA256 format</description>
        /// </returns>
        #endregion

        #region Root main recovery password reader method
        /// <summary>
        /// Read root recovery password from root part of config file
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>In SHA256 format</description>
        /// </returns>
        #endregion
        #region Root recovery hint string reader method
        /// <summary>
        /// Read root recovery hint string from root part of config file
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>A hint string for recovery password</description>
        /// </returns>
        #endregion
    }
    #endregion
    #region Service part of config file class
    /// <summary>
    /// Contains methods which are related to service part of config file
    /// </summary>
    public static class ServicePart
    {
        #region Reading Config file
        /// <summary>
        /// Content of service part in config file
        /// </summary>
        /// <remarks>
        /// Each part has its index number
        /// </remarks>
        public static string[] ConfigFileParted { get; } =
            Regex
                .Replace(
                    ConfigFile.ConfigFileReader()[
                        ConfigFile.ConfigLinesNumberDictionary["Service"]
                    ],
                    @"(^{)|(}$)",
                    ""
                )
                .Split(';');
        #endregion

        #region Config file parts dictionary
        /// <summary>
        /// Main parts of service part in config file with their index
        /// </summary>
        private static readonly Dictionary<string, int> ConfigPartsNumbersDictionary =
            new Dictionary<string, int>()
            {
                { "Status", 0 },
                { "StartTimeOfDay", 2 },
                { "EndTimeOfDay", 4 },
                { "AllowedDuration", 6 },
                { "TempAllowedDuration", 8 }
            };
        #endregion

        #region Status reader method
        /// <summary>
        /// Status of application service from service part of config file
        /// </summary>
        /// <returns>
        ///     <term>Boolean</term>
        ///     <description>True for installed service | False for not installed service</description>
        /// </returns>
        public static bool Status()
        {
            if (
                Hashing.Hash.ToSha256("1")
                == ConfigFileParted[ConfigPartsNumbersDictionary["Status"]]
            )
                return true;
            else
                return false;
        }
        #endregion

        #region Start time of day reader method
        /// <summary>
        /// Read start time of day from service part of config file
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>In form of [Two digit number]:[Two digit number]</description>
        /// </returns>
        public static string StartTimeOfDay()
        {
            return ConfigFileParted[ConfigPartsNumbersDictionary["StartTimeOfDay"]];
        }
        #endregion

        #region End time of day reader method
        /// <summary>
        /// Read end time of day from service part of config file
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>In form of [Two digit number]:[Two digit number]</description>
        /// </returns>
        public static string EndTimeOfDay()
        {
            return ConfigFileParted[ConfigPartsNumbersDictionary["EndTimeOfDay"]];
        }
        #endregion

        #region Allowed duration reader method
        /// <summary>
        /// Read allowed duration from service part of config file
        /// </summary>
        /// <returns>
        ///     <term>Integer</term>
        ///     <description>Number is in minutes</description>
        /// </returns>
        public static int AllowedDuration()
        {
            return int.Parse(ConfigFileParted[ConfigPartsNumbersDictionary["AllowedDuration"]]);
        }
        #endregion

        #region Template allowed duration reader method
        /// <summary>
        /// Read template allowed duration from service part of config file
        /// </summary>
        /// <returns>
        ///     <term>Integer</term>
        ///     <description>Number is in minutes</description>
        /// </returns>
        public static int TempAllowedDuration()
        {
            return int.Parse(ConfigFileParted[ConfigPartsNumbersDictionary["TempAllowedDuration"]]);
        }
        #endregion
    }
    #endregion
}
