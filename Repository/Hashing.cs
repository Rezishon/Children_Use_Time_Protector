using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hashing
{
    public static class Hash
    {
        public static string ToSha256(string String)
        {
            using var mysha256 = SHA256.Create();
            byte[] messageBytes = Encoding.UTF8.GetBytes(String);
            byte[] hashValue = SHA256.HashData(messageBytes);
            return BitConverter.ToString(hashValue).Replace("-", string.Empty);
        }

        public static bool CompareSha256s(string strF, string strS)
        {
            return string.Equals(strF, strS);
        }
    }
}
