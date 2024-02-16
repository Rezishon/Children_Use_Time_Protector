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
    }
}
