using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileEncrypter
{
    public class NetCryptography : ICryptography
    {
        public byte[] HashPassword(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes(Constants.DefaultSalt);
            byte[] encryptionKey = null;
            try
            {
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, Constants.DefaultIterations, HashAlgorithmName.SHA256);
                encryptionKey = key.GetBytes(32);
            } catch (Exception e)
            {
                Console.WriteLine("Error ", e);
            }

            return encryptionKey;
        }
    }
}
