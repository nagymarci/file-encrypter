using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileEncrypter
{
    public class FileCrypter
    {
        private readonly ICryptography cryptography;
        private readonly IFileSystem fileSystem;

        public string InputFile { get; set; }
        public string OutputFile { get; set; }

        public FileCrypter(ICryptography crypto, IFileSystem fs)
        {
            cryptography = crypto;
            fileSystem = fs;
        }

        public bool Encrypt(string password, string inputFile, string outFilePath)
        {
            try
            {
                var encryptionKey = cryptography.HashPassword(password);
                var fileContent = fileSystem.File.ReadAllBytes(inputFile);
                string filename = fileSystem.Path.GetFileName(inputFile);
                var fileNameBytes = Encoding.ASCII.GetBytes(filename);
                var fileContentWithFileName = BitConverter.GetBytes(fileNameBytes.Length);
                fileContentWithFileName = fileContentWithFileName.Concat(fileNameBytes).ToArray();
                fileContentWithFileName = fileContentWithFileName.Concat(fileContent).ToArray();

                var encryptedContent = cryptography.Encrypt(encryptionKey, fileContentWithFileName);
                if (!fileSystem.Directory.Exists(outFilePath))
                {
                    fileSystem.Directory.CreateDirectory(outFilePath);
                }
                fileSystem.File.WriteAllBytes(fileSystem.Path.Combine(outFilePath, filename + Constants.DefaultFileExtension), encryptedContent);
            } catch (Exception e)
            {
                Console.Write($"Failed to encrypt file {inputFile}." + e);
                return false;
            }
            return true;
        }

        public bool Decrypt(string password, string inputFile, string outFilePath)
        {
            try
            {
                var encryptionKey = cryptography.HashPassword(password);
                var fileContent = fileSystem.File.ReadAllBytes(inputFile);

                var decryptedContent = cryptography.Decrypt(encryptionKey, fileContent);
                int fileNameSize = BitConverter.ToInt32(decryptedContent, 0);
                string filename = Encoding.ASCII.GetString(decryptedContent, sizeof(int), fileNameSize);
                byte[] content = new byte[decryptedContent.Length - fileNameSize - sizeof(int)];
                Array.Copy(decryptedContent, fileNameSize + sizeof(int), content, 0, content.Length);
                if (!fileSystem.Directory.Exists(outFilePath))
                {
                    fileSystem.Directory.CreateDirectory(outFilePath);
                }
                fileSystem.File.WriteAllBytes(fileSystem.Path.Combine(outFilePath, filename), content);
            } catch (Exception e)
            {
                Console.Write($"Failed to decrypt file {inputFile}. " + e);
                return false;
            }
            return true;
        }
    }
}
