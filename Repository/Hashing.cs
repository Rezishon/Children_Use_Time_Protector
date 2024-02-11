using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Children_Use_Time_Protector.Repository
{
    public class Hashing
    {
        public static string ToSha256(string messageString)
        {
            using var mysha256 = SHA256.Create();
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageString);
            byte[] hashValue = SHA256.HashData(messageBytes);
            return BitConverter.ToString(hashValue).Replace("-", string.Empty);
        }
    }
}
