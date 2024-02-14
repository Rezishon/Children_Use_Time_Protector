using System.Security.Cryptography;
using System.Text;

namespace Children_Use_Time_Protector;

class Program
{
    static void Main(string[] args)
    {
        using var mysha256 = SHA256.Create();
        byte[] messageBytes = Encoding.UTF8.GetBytes("reza");
        byte[] hashValue = SHA256.HashData(messageBytes);
        var result = BitConverter.ToString(hashValue).Replace("-", string.Empty);
        // Hashing hashing = new Hashing("reza");
        Console.WriteLine(result);
    }
}
