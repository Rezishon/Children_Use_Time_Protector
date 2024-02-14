namespace ConfigHandling
{
    public static class ConfigFile
    {
        public static string ConfigFilePath { get; } = "./Config.cutp";
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
    }

    public static class RootPart
    {
        public async Task<bool> ReadAppStatus()
        {
            Exist_Of_Database_File();

            string[] linesOfText = await File.ReadAllLinesAsync(ConfigFile.ConfigFilePath);
            return int.Parse(linesOfText[0].Trim()) == 0;
        }

        public async void Exist_Of_Database_File()
        {
            if (!File.Exists(ConfigFile.ConfigFilePath))
            {
                await File.WriteAllTextAsync(ConfigFile.ConfigFilePath, "0");
                // ask for root password
            }
        }
    }
}
