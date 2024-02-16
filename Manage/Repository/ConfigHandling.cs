using System.Text.RegularExpressions;

namespace ConfigHandling
{
    public static class ConfigFile
    {
        public static Dictionary<string, int> ConfigLinesNumber = new Dictionary<string, int>()
        {
            { "Root", 0 },
            { "Service", 1 }
        };
        public static string ConfigFilePath { get; } =
            @"..\..\..\..\Manage\bin\Debug\net8.0\Config.cutp";
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
            catch (System.Exception e)
            {
                System.Console.WriteLine(e);
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
        public static string[] ConfigFileParted { get; } =
            Regex
                .Replace(
                    ConfigFile.ConfigFileReader()[ConfigFile.ConfigLinesNumber["Service"]],
                    @"(^{)|(}$)",
                    ""
                )
                .Split(';');
        private static Dictionary<string, int> ConfigPartsNumbers = new Dictionary<string, int>()
        {
            { "Status", 0 },
            { "StartTimeOfDay", 2 },
            { "EndTimeOfDay", 4 },
            { "AllowedDuration", 6 },
            { "TempAllowedDuration", 8 }
        };

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
    }
}
