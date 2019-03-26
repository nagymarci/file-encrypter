using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileEncrypter
{
    public class NetCryptography : ICryptography
    {
        public byte[] Decrypt(byte[] encryptionKey, byte[] input)
        {
            byte[] decrypted = null;

            using (RijndaelManaged aesAlg = new RijndaelManaged())
            {
                aesAlg.Key = encryptionKey;
                aesAlg.BlockSize = Constants.DefaultAesBlockSize;
                using (MemoryStream memory = new MemoryStream())
                {
                    byte[] iv = new byte[Constants.DefaultAesIVSize];
                    Array.Copy(input, iv, iv.LongLength);
                    using (CryptoStream cryptoStream = new CryptoStream(memory, aesAlg.CreateDecryptor(encryptionKey, iv), CryptoStreamMode.Write))
                    {
                        using (BinaryWriter stream = new BinaryWriter(cryptoStream))
                        {
                            stream.Write(input, iv.Length, input.Length - iv.Length);
                        }
                        decrypted = memory.ToArray();
                    }
                }
            }

            return decrypted;
        }

        public byte[] Encrypt(byte[] encryptionKey, byte[] input)
        {
            byte[] encrypted = null;

            using (RijndaelManaged aesAlg = new RijndaelManaged())
            {
                aesAlg.Key = encryptionKey;
                aesAlg.GenerateIV();
                aesAlg.BlockSize = Constants.DefaultAesBlockSize;
                using (MemoryStream memory = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memory, aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV), CryptoStreamMode.Write))
                    {
                        memory.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                        using (BinaryWriter stream = new BinaryWriter(cryptoStream))
                        {
                            stream.Write(input);
                        }
                        encrypted = memory.ToArray();
                    }
                }
            }

            return encrypted;
        }

        public byte[] HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password), "Null or empty");
            }

            byte[] salt = Constants.DefaultSaltBytes;
            byte[] encryptionKey = null;
            using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, Constants.DefaultIterations, HashAlgorithmName.SHA256))
            {
                encryptionKey = key.GetBytes(32);
            }

            return encryptionKey;
        }
    }
}
