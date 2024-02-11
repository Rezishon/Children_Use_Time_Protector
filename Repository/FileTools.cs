using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Children_Use_Time_Protector.Repository
{
    public class FileTools(string Path = "./DataBase.cutp")
    {
        private readonly string path = Path;
    }
}
