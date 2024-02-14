using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConfigHandling
{
    public class FileTools(string Path = "./DataBase.cutp")
    {
        private readonly string path = Path;

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
