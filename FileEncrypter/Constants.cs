using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileEncrypter
{
    static class Constants
    {
        static public string DefaultSalt { get; } = "878353BC595A4E2CA430F3CE12CB3690";
        static public byte[] DefaultSaltBytes { get; } = StringToByteArray(DefaultSalt);
        static public int DefaultIterations { get; } = 1000000;
        static public int DefaultAesBlockSize { get; } = 256;
        static public int DefaultAesIVSize { get; } = DefaultAesBlockSize / 8;
        static public string DefaultFileExtension { get; } = ".fenc";

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
