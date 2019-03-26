using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileEncrypter
{
    static class CommandLineParser
    {
        public class ArgumentParseException : Exception
        {
            public ArgumentParseException(string s) : base(s) { }
        }

        public static bool Encrypt { get; private set; } = true;
        public static string InputFile { get; private set; } = null;
        public static string OutputFolder { get; private set; } = null;

        public static void Parse(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg.Equals("-e") || arg.Equals("--encrypt") || arg.Equals("Encrypt"))
                {
                    Encrypt = true;
                }

                if (arg.Equals("-d") || arg.Equals("--decrypt") || arg.Equals("Decrypt"))
                {
                    Encrypt = false;
                }

                if (arg.StartsWith("-f=") || arg.StartsWith("--file=") || arg.StartsWith("File="))
                {
                    InputFile = arg.Split("=".ToCharArray())[1];
                }

                if (arg.StartsWith("-o=") || arg.StartsWith("--outfile=") || arg.StartsWith("OutFolder="))
                {
                    OutputFolder = arg.Split("=".ToCharArray())[1];
                }
            }

            if (string.IsNullOrWhiteSpace(InputFile) || string.IsNullOrWhiteSpace(OutputFolder))
            {
                throw new ArgumentParseException("Input or output is empty");
            }
        }
    }
}
