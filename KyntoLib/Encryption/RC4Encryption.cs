using System;
using System.Text;

namespace KyntoLib.Encryption
{
    /// <summary>
    /// Handles RC4 Encryption.
    /// </summary>
    public static class RC4Encryption
    {
        /// <summary>
        /// Encrypts the specified data.
        /// </summary>
        /// <param name="ToDecrypt">The data to encrypt.</param>
        /// <param name="EncryptionKey">The key to encrypt using.</param>
        /// <returns>The encrypted version of the input data.</returns>
        public static string Decrypt(string ToDecrypt, string EncryptionKey)
        {
            Byte[] bytes = Convert.FromBase64String(ToDecrypt);
            Byte[] key = Encoding.ASCII.GetBytes(EncryptionKey);

            Byte[] s = new Byte[256];
            Byte[] k = new Byte[256];
            Byte temp;
            int i, j;

            for (i = 0; i < 256; i++)
            {
                s[i] = (Byte)i;
                k[i] = key[i % key.GetLength(0)];
            }

            j = 0;
            for (i = 0; i < 256; i++)
            {
                j = (j + s[i] + k[i]) % 256;
                temp = s[i];
                s[i] = s[j];
                s[j] = temp;
            }

            i = j = 0;
            for (int x = 0; x < bytes.GetLength(0); x++)
            {
                i = (i + 1) % 256;
                j = (j + s[i]) % 256;
                temp = s[i];
                s[i] = s[j];
                s[j] = temp;
                int t = (s[i] + s[j]) % 256;
                bytes[x] ^= s[t];
            }

            return Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// Decrypts the input data.
        /// </summary>
        /// <param name="ToEncrypt">The data to decrypt.</param>
        /// <param name="EncryptionKey">The key to decrypt using.</param>
        /// <returns>The decrypted version of the input string.</returns>
        public static string Encrypt(string ToEncrypt, string EncryptionKey)
        {
            Byte[] bytes = Encoding.ASCII.GetBytes(ToEncrypt);
            Byte[] key = Encoding.ASCII.GetBytes(EncryptionKey);

            Byte[] s = new Byte[256];
            Byte[] k = new Byte[256];
            Byte temp;
            int i, j;

            for (i = 0; i < 256; i++)
            {
                s[i] = (Byte)i;
                k[i] = key[i % key.GetLength(0)];
            }

            j = 0;
            for (i = 0; i < 256; i++)
            {
                j = (j + s[i] + k[i]) % 256;
                temp = s[i];
                s[i] = s[j];
                s[j] = temp;
            }

            i = j = 0;
            for (int x = 0; x < bytes.GetLength(0); x++)
            {
                i = (i + 1) % 256;
                j = (j + s[i]) % 256;
                temp = s[i];
                s[i] = s[j];
                s[j] = temp;
                int t = (s[i] + s[j]) % 256;
                bytes[x] ^= s[t];
            }

            return Convert.ToBase64String(bytes);
        }
    }
}
