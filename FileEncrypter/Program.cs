using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FileEncrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CommandLineParser.Parse(args);
            }
            catch (CommandLineParser.ArgumentParseException e)
            {
                Console.WriteLine("Exception when reading arguments. " + e.Message);
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("Give encryption key: ");
            //var encryptionKey = Console.ReadLine();
            var encryptionKey = ReadEncryptionKey();
            //Console.WriteLine("Give encryption key again: ");
            /*using (var encryptionKeyAgain = ReadEncryptionKey())
            {
                if (!encryptionKey.ToString().Equals(encryptionKeyAgain.ToString()))
                {
                    Console.WriteLine("not matching string");
                    Console.ReadKey(true);
                    return;
                }
            }*/
            /*var encryptionKeyAgain = ReadEncryptionKey();
            if (string.IsNullOrWhiteSpace(encryptionKey) || !encryptionKey.Equals(encryptionKeyAgain))
            {
                Console.WriteLine("not matching or empty string");
                Console.ReadKey(true);
                return;
            }*/

            Console.ReadKey(true);
        }

        private static string ReadEncryptionKey()
        {
            string encryptionKey = "";
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);
                // Ignore if Alt or Ctrl is pressed.
                if ((keyInfo.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt)
                    continue;
                if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
                    continue;
                // Ignore if KeyChar value is \u0000.
                if (keyInfo.KeyChar == '\u0000') continue;
                // Ignore tab key.
                if (keyInfo.Key == ConsoleKey.Tab) continue;
                // Handle backspace.
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    continue;
                }
                // Handle Escape key.
                if (keyInfo.Key == ConsoleKey.Escape) continue;
                // Handle key by adding it to input string.
                //Console.Write(keyInfo.KeyChar);
                encryptionKey += (keyInfo.KeyChar);
                //Console.Write("*");
            } while (keyInfo.Key != ConsoleKey.Enter);

            return encryptionKey;
        }
    }
}