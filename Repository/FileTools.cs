using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConfigHandling
{
    public static class ConfigFile
    {
        public static string ConfigFilePath { get; set; }
        private static readonly string NullString = "*";

        static ConfigFile()
        {
            ConfigFilePath = "./Config.cutp";
        }

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
        public async Task<bool> ReadAppStatus()
        {
            Exist_Of_Database_File();

            string[] linesOfText = await File.ReadAllLinesAsync(path);
            return int.Parse(linesOfText[0].Trim()) == 0;
        }

        public async void Exist_Of_Database_File()
        {
            if (!File.Exists(path))
            {
                await File.WriteAllTextAsync(path, "0");
                // ask for root password
            }
        }
    }
}
