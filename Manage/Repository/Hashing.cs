using System.Security.Cryptography;
using System.Text;

namespace Hashing
{
    /// <summary>
    /// Generate SHA256 hash strings
    /// </summary>
    public static class Hash
    {
        public static string ToSha256(string String)
        {
            using var mysha256 = SHA256.Create();
            byte[] messageBytes = Encoding.UTF8.GetBytes(String);
            byte[] hashValue = SHA256.HashData(messageBytes);
            return BitConverter.ToString(hashValue).Replace("-", string.Empty);
        }
    }
}
