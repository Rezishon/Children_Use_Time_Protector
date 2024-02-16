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
}
