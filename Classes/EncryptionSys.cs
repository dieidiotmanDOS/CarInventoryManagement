using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CarInventoryManagement.Classes
{
    public static class EncryptionSys
    {
        public static string Encrypt(string plaintext)
        {
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                byte[] keys = Encoding.UTF8.GetBytes("A82D92I2");
                // Sets the key for the DES encryption. This was chosen by Mr Holder previously.

                ICryptoTransform encryptor = provider.CreateEncryptor(keys, keys);
                // Creates the encryptor object that will be used to encrypt the plain text.

                var ms = new MemoryStream();
                // Allocates a new memory stream.
                var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                // Allocates memory for the cryptography stream.

                byte[] input = Encoding.UTF8.GetBytes(plaintext);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
                // Returns the result as a Base64String from a hexadecimal value.

                // This code as provided by: https://github.com/hamza3344/txtloginhash/blob/main/txtfilelogin.zip

            }
        }
    }
}
