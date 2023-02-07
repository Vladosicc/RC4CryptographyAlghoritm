using System;
using System.Security.Cryptography;

namespace RC4Cryptography
{
    /// <summary>
    /// Class for encrypt and decrypt byte array
    /// </summary>
    public class RC4Cipher
    {
        /// <summary>
        /// Encrypt byte array
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">data</param>
        /// <exception cref="DivideByZeroException">Key lenght = 0</exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <example>var enc = rc4Cipher.Encrypt(new byte[]{1,2,3}, new byte[]{255,254,253});</example>
        /// <returns>Encrypted byte array</returns>
        public byte[] Encrypt(byte[] key, byte[] data)
        {
            //Output bytes
            byte[] encrypted;

            using (var RC4 = new RC4CryptoProvider())
            {
                var encryptor = RC4.CreateEncryptor(key, null);

                // Create the streams used for encryption. 
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(data, 0, data.Length);
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        /// <summary>
        /// Decrypt byte array
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">data</param>
        /// <exception cref="DivideByZeroException">Key lenght = 0</exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <example>var dec = rc4Cipher.Decrypt(new byte[]{1,2,3}, new byte[]{255,254,253});</example>
        /// <returns>Decrypted byte array</returns>
        public byte[] Decrypt(byte[] key, byte[] data)
        {
            byte[] decrypted;

            using (var RC4 = new RC4CryptoProvider())
            {
                using (var input = new MemoryStream(data))
                using (var output = new MemoryStream())
                {
                    var decryptor = RC4.CreateDecryptor(key, null);
                    using (var cryptStream = new CryptoStream(input, decryptor, CryptoStreamMode.Read))
                    {
                        var buffer = new byte[1024];
                        var read = cryptStream.Read(buffer, 0, buffer.Length);
                        while (read > 0)
                        {
                            output.Write(buffer, 0, read);
                            read = cryptStream.Read(buffer, 0, buffer.Length);
                        }
                        cryptStream.Flush();
                    }
                    decrypted = output.ToArray();
                }

            }

            return decrypted;
        }
    }
}
