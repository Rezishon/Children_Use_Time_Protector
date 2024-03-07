using System.Text.RegularExpressions;
using CommandHandling;

namespace ConfigHandling
{
    /// <summary>
    /// Contains methods which are related to config file itself
    /// </summary>
    public static class ConfigFile
    {
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

        /// <summary>
        /// Config file path comes from application path with some customization
        /// </summary>
        public static string ConfigFilePath { get; } =
            Regex.Replace(
                Commands.Pwd(),
                @"\\Children_Use_Time_Protector\\(\w*\W*)*$",
                @"\Children_Use_Time_Protector\Manage\bin\Debug\net8.0\Config.cutp"
            );
        private static string NullString { get; } = "*";

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
    }
        /// <summary>
        /// Main parts of root part in config file with their index
        /// </summary>
        /// <summary>
        /// Status of root from root part of config file
        /// </summary>
        /// <returns>
        ///     <term>Boolean</term>
        ///     <description>True for available root password | False for not available root password</description>
        /// </returns>

    // public static class RootPart
    // {
    //     public async Task<bool> ReadAppStatus()
    //     {
    //         Exist_Of_Database_File();

    //         string[] linesOfText = await File.ReadAllLinesAsync(ConfigFile.ConfigFilePath);
    //         return int.Parse(linesOfText[0].Trim()) == 0;
    //     }

    //     public async void Exist_Of_Database_File()
    //     {
    //         if (!File.Exists(ConfigFile.ConfigFilePath))
    //         {
    //             await File.WriteAllTextAsync(ConfigFile.ConfigFilePath, "0");
    //             // ask for root password
    //         }
    //     }
    // }
        /// <summary>
        /// Read root main password from root part of config file
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>In SHA256 format</description>
        /// </returns>

        /// <summary>
        /// Read root recovery password from root part of config file
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>In SHA256 format</description>
        /// </returns>
        /// <summary>
        /// Read root recovery hint string from root part of config file
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>A hint string for recovery password</description>
        /// </returns>
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
        private static readonly Dictionary<string, int> ConfigPartsNumbers = new Dictionary<
            string,
            int
        >()
        {
            { "Status", 0 },
            { "StartTimeOfDay", 2 },
            { "EndTimeOfDay", 4 },
            { "AllowedDuration", 6 },
            { "TempAllowedDuration", 8 }
        };
        /// <summary>
        /// Main parts of service part in config file with their index
        /// </summary>
        #endregion

        #region Return config file parts
        /// <summary>
        /// Status of application service from service part of config file
        /// </summary>
        /// <returns>
        ///     <term>Boolean</term>
        ///     <description>True for installed service | False for not installed service</description>
        /// </returns>
        public static bool Status()
        {
            if (Hashing.Hash.ToSha256("1") == ConfigFileParted[ConfigPartsNumbers["Status"]])
                return true;
            else
                return false;
        }

        /// <summary>
        /// Read start time of day from service part of config file
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>In form of [Two digit number]:[Two digit number]</description>
        /// </returns>
        public static string StartTimeOfDay()
        {
            return ConfigFileParted[ConfigPartsNumbers["StartTimeOfDay"]];
        }

        /// <summary>
        /// Read end time of day from service part of config file
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>In form of [Two digit number]:[Two digit number]</description>
        /// </returns>
        public static string EndTimeOfDay()
        {
            return ConfigFileParted[ConfigPartsNumbers["EndTimeOfDay"]];
        }

        /// <summary>
        /// Read allowed duration from service part of config file
        /// </summary>
        /// <returns>
        ///     <term>Integer</term>
        ///     <description>Number is in minutes</description>
        /// </returns>
        public static int AllowedDuration()
        {
            return int.Parse(ConfigFileParted[ConfigPartsNumbers["AllowedDuration"]]);
        }

        /// <summary>
        /// Read template allowed duration from service part of config file
        /// </summary>
        /// <returns>
        ///     <term>Integer</term>
        ///     <description>Number is in minutes</description>
        /// </returns>
        public static int TempAllowedDuration()
        {
            return int.Parse(ConfigFileParted[ConfigPartsNumbers["TempAllowedDuration"]]);
        }
        #endregion
    }
}
