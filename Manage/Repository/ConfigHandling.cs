using System.Text.RegularExpressions;
using CommandHandling;

namespace ConfigHandling
{
    public static class ConfigFile
    {
        public static Dictionary<string, int> ConfigLinesNumberDictionary = new Dictionary<
            string,
            int
        >()
        {
            { "Root", 0 },
            { "Service", 1 }
        };

        public static string ConfigFilePath { get; } =
            Regex.Replace(
                Commands.Pwd(),
                @"\\Children_Use_Time_Protector\\(\w*\W*)*$",
                @"\Children_Use_Time_Protector\Manage\bin\Debug\net8.0\Config.cutp"
            );
        private static string NullString { get; } = "*";

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

        public static string[] ConfigFileReader()
        {
            return File.ReadAllLines(ConfigFilePath);
        }
    }

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

    public static class ServicePart
    {
        #region Reading Config file
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
        #endregion

        #region Return config file parts
        public static bool Status()
        {
            if (Hashing.Hash.ToSha256("1") == ConfigFileParted[ConfigPartsNumbers["Status"]])
                return true;
            else
                return false;
        }

        public static string StartTimeOfDay()
        {
            return ConfigFileParted[ConfigPartsNumbers["StartTimeOfDay"]];
        }

        public static string EndTimeOfDay()
        {
            return ConfigFileParted[ConfigPartsNumbers["EndTimeOfDay"]];
        }

        public static int AllowedDuration()
        {
            return int.Parse(ConfigFileParted[ConfigPartsNumbers["AllowedDuration"]]);
        }

        public static int TempAllowedDuration()
        {
            return int.Parse(ConfigFileParted[ConfigPartsNumbers["TempAllowedDuration"]]);
        }
        #endregion
    }
}
