using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileEncrypter
{
    public interface ICryptography
    {
        byte[] HashPassword(string password);
        byte[] Encrypt(byte[] encryptionKey, byte[] input);
        byte[] Decrypt(byte[] encryptionKey, byte[] input);
    }
}
