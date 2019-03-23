using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileEncrypter
{
    class FileCrypter
    {
        private readonly ICryptography cryptography;

        public string InputFile { get; set; }
        public string OutputFile { get; set; }

        public FileCrypter(ICryptography crypto)
        {
            cryptography = crypto;
        }

        public void Encrypt(string password, string inputFile, string outFile)
        {

        }
    }
}
