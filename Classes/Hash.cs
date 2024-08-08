using System.Security.Cryptography;
using System.Text;

namespace CarInventoryManagement.Classes
{
    public static class Hash
    {
        public static string ToSHA256(string inputStr)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputStr));
            // Using hashing method of SHA256, the string is recieved and is computed as through the UTF8 encoder into an array of bytes.

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i].ToString("x2"));
            }
            // Each byte after being hashed is then collected into a string builder which will create the hashed result.

            return stringBuilder.ToString();
        }
    }
}
