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

        #region Config file builder method
        /// <summary>
        /// Make raw config file
        /// </summary>
        /// <remarks>Used for first run of application</remarks>
        public static void ConfigFileBuilder()
        {
            try
            {
                string RootPart = $"{Hashing.Hash.ToSha256("0")};;;;";
                string ServicePart = $"{Hashing.Hash.ToSha256("0")};04:00;;20:00;;120;;120;0;";

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

        #region Normal content method
        /// <summary>
        /// The method returns the parts of wanted line of config file
        /// </summary>
        /// <param name="lineName">The line name you want like Root, Service, and etc.</param>
        /// <returns>
        ///     <term>Array of string</term>
        ///     <description>Each index represents each part of wanted line</description>
        /// </returns>
        public static string[] NormalContent(string lineName)
        {
            return Regex
                .Replace(
                    ConfigFile.ConfigFileReader()[ConfigFile.ConfigLinesNumberDictionary[lineName]],
                    @"(^{)|(}$)",
                    ""
                )
                .Split(';');
        }

        /// <summary>
        /// Convert the content into part by part array
        /// </summary>
        /// <param name="content">The string array which contains config data</param>
        /// <param name="lineName">Name of config line you want</param>
        /// <returns>
        ///     <term>String array</term>
        ///     <description>Each index represents each part</description>
        /// </returns>
        public static string[] NormalContent(string[] content, string lineName)
        {
            return Regex
                .Replace(
                    content[ConfigFile.ConfigLinesNumberDictionary[lineName]],
                    @"(^{)|(}$)",
                    ""
                )
                .Split(';');
        }
        #endregion
    }
    #endregion

    #region Root part of config file class
    public static class RootPart
    {
        #region Reading Config file
        private static string ConfigFileParted(string PartName)
        {
            return ConfigFile.NormalContent("Root")[ConfigPartsNumbersDictionary[PartName]];
        }
        #endregion

        #region Config file parts dictionary
        /// <summary>
        /// Main parts of root part in config file with their index
        /// </summary>
        public static readonly Dictionary<string, int> ConfigPartsNumbersDictionary =
            new Dictionary<string, int>()
            {
                { "Status", 0 },
                { "RootMainPassword", 1 },
                { "RootRecoveryPassword", 2 },
                { "RecoveryHintString", 3 }
            };
        #endregion

        #region Status reader method
        /// <summary>
        /// Status of root from root part of config file
        /// </summary>
        /// <returns>
        ///     <term>Boolean</term>
        ///     <description>True for available root password | False for not available root password</description>
        /// </returns>
        public static bool Status()
        {
            if (Hashing.Hash.ToSha256("1") == ConfigFileParted("Status"))
                return true;
            else
                return false;
        }
        #endregion

        #region Root main password reader method
        /// <summary>
        /// Read root main password from root part of config file
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>In SHA256 format</description>
        /// </returns>
        public static string RootMainPassword()
        {
            return ConfigFileParted("RootMainPassword");
        }
        #endregion

        #region Root recovery password reader method
        /// <summary>
        /// Read root recovery password from root part of config file
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>In SHA256 format</description>
        /// </returns>
        public static string RootRecoveryPassword()
        {
            return ConfigFileParted("RootRecoveryPassword");
        }
        #endregion

        #region Root recovery hint string reader method
        /// <summary>
        /// Read root recovery hint string from root part of config file
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>A hint string for recovery password</description>
        /// </returns>
        public static string RootRecoveryHintString()
        {
            return ConfigFileParted("RecoveryHintString");
        }
        #endregion
    }
    #endregion

    #region Service part of config file class
    /// <summary>
    /// Contains methods which are related to service part of config file
    /// </summary>
    public class ServicePart
    {
        #region Reading Config file
        /// <summary>
        /// Content of service part in config file
        /// </summary>
        /// <remarks>
        /// Each part has its index number
        /// </remarks>
        private static string ConfigFileParted(string PartName)
        {
            return ConfigFile.NormalContent("Service")[ConfigPartsNumbersDictionary[PartName]];
        }
        #endregion

        #region Config file parts dictionary
        /// <summary>
        /// Main parts of service part in config file with their index
        /// </summary>
        public static readonly Dictionary<string, int> ConfigPartsNumbersDictionary =
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
            if (Hashing.Hash.ToSha256("1") == ConfigFileParted("Status"))
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
            return ConfigFileParted("StartTimeOfDay");
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
            return ConfigFileParted("EndTimeOfDay");
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
            return int.Parse(ConfigFileParted("AllowedDuration"));
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
            return int.Parse(ConfigFileParted("TempAllowedDuration"));
        }
        #endregion
    }
    #endregion

    #region Config setter Class
    public class ConfigSetter
    {
        public static void SetThisConfig(string configContent, string lineName, string partName)
        {
            switch (ConfigFile.ConfigLinesNumberDictionary[lineName])
            {
                // Root
                case 0:
                    // Read data of config file
                    string[] configFileContentRootCase = ConfigFile.ConfigFileReader();

                    // Separate content parts
                    string[] resultRootCase = ConfigFile.NormalContent(
                        configFileContentRootCase,
                        "Root"
                    );

                    // Replace wanted value
                    resultRootCase[RootPart.ConfigPartsNumbersDictionary[partName]] = configContent;

                    // Reform value => add ; between parts & { at first index
                    configFileContentRootCase[ConfigFile.ConfigLinesNumberDictionary["Root"]] =
                        string.Join(';', resultRootCase).Insert(0, "{");

                    // Reform value => add } at end index
                    configFileContentRootCase[ConfigFile.ConfigLinesNumberDictionary["Root"]] =
                        configFileContentRootCase[
                            ConfigFile.ConfigLinesNumberDictionary["Root"]
                        ].Insert(
                            configFileContentRootCase[
                                ConfigFile.ConfigLinesNumberDictionary["Root"]
                            ].Length,
                            "}"
                        );

                    // Print to config file
                    File.WriteAllText(
                        ConfigFile.ConfigFilePath,
                        string.Join("\n", configFileContentRootCase)
                    );
                    break;

                // Service
                case 1:
                    // Read data of config file
                    string[] configFileContentServiceCase = ConfigFile.ConfigFileReader();

                    // Separate content parts
                    string[] resultServiceCase = ConfigFile.NormalContent(
                        configFileContentServiceCase,
                        "Service"
                    );

                    // Replace wanted value
                    resultServiceCase[ServicePart.ConfigPartsNumbersDictionary[partName]] =
                        configContent;

                    // Reform value => add ; between parts & { at first index
                    configFileContentServiceCase[
                        ConfigFile.ConfigLinesNumberDictionary["Service"]
                    ] = string.Join(';', resultServiceCase).Insert(0, "{");

                    // Reform value => add } at end index
                    configFileContentServiceCase[
                        ConfigFile.ConfigLinesNumberDictionary["Service"]
                    ] = configFileContentServiceCase[
                        ConfigFile.ConfigLinesNumberDictionary["Service"]
                    ].Insert(
                        configFileContentServiceCase[
                            ConfigFile.ConfigLinesNumberDictionary["Service"]
                        ].Length,
                        "}"
                    );

                    // Print to config file
                    File.WriteAllText(
                        ConfigFile.ConfigFilePath,
                        string.Join("\n", configFileContentServiceCase)
                    );
                    break;
            }
        }

        public class SetConfigToRoot
        {
            public static void Status(string configContent)
            {
                SetThisConfig(configContent, "Root", "Status");
            }

            public static void RootMainPassword(string configContent)
            {
                SetThisConfig(configContent, "Root", "RootMainPassword");
            }

            public static void RootRecoveryPassword(string configContent)
            {
                SetThisConfig(configContent, "Root", "RootRecoveryPassword");
            }

            public static void RecoveryHintString(string configContent)
            {
                SetThisConfig(configContent, "Root", "RecoveryHintString");
            }
        }

        public class SetConfigToService
        {
            // All methods pass the line name service
            // for each part name we should have a method separately which the lineName is service and the the partName would be the method name
            public static void Status(string configContent)
            {
                SetThisConfig(configContent, "Service", "Status");
            }

            public static void StartTimeOfDay(string configContent)
            {
                SetThisConfig(configContent, "Service", "StartTimeOfDay");
            }

            public static void EndTimeOfDay(string configContent)
            {
                SetThisConfig(configContent, "Service", "EndTimeOfDay");
            }
        }
    }
    #endregion
}
