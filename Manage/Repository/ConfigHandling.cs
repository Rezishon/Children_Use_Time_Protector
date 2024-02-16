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
        }
    }
}
