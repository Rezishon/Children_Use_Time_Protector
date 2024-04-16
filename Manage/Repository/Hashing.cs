using System.Security.Cryptography;
using System.Text;

namespace Children_Use_Time_Protector.Repository.Hashing
{
    #region Hash class
    /// <summary>
    /// Generate SHA256 hash strings
    /// </summary>
    public static class Hash
    {
        #region SHA256 generator
        /// <summary>
        /// Generate SHA256 string from its parameter
        /// </summary>
        /// <param name="String">The String you want its hash string</param>
        /// <returns>
        ///     <term>String</term>
        ///     <description>256 character string - SHA256 hash string</description>
        /// </returns>
        public static string ToSha256(string String)
        {
            using var mysha256 = SHA256.Create();
            byte[] messageBytes = Encoding.UTF8.GetBytes(String);
            byte[] hashValue = SHA256.HashData(messageBytes);
            return BitConverter.ToString(hashValue).Replace("-", string.Empty);
        }
        #endregion
    }
    #endregion
}
