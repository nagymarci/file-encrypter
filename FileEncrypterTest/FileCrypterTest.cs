using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileEncrypter;
using System.Text;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.Linq;

namespace FileEncrypterTest
{
    [TestClass]
    public class FileCrypterTest
    {
        private class Mock_Cryptography : ICryptography
        {
            public static string EncryptedText { get; } = "This is the ENcrypted text!";
            public static string DecryptedText { get; } = "This is the DEcrypted text!!";
            public static string HashedPassword { get; } = "hashedPassword";
            public static byte[] EncryptedBytes { get; } = Encoding.ASCII.GetBytes(EncryptedText);
            public static byte[] DecryptedBytes { get; } = Encoding.ASCII.GetBytes(DecryptedText);
            public static byte[] HashedPasswordBytes { get; } = Encoding.ASCII.GetBytes(HashedPassword);

            public byte[] FileContent { get; set; }


            public byte[] Decrypt(byte[] encryptionKey, byte[] input)
            {
                return FileContent;
            }

            public byte[] Encrypt(byte[] encryptionKey, byte[] input)
            {
                return EncryptedBytes;
            }

            public byte[] HashPassword(string password)
            {
                return HashedPasswordBytes;
            }
        }
        private Mock_Cryptography mock_Cryptography = new Mock_Cryptography();
        private MockFileSystem mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            {@"C:\input.txt", new MockFileData(Mock_Cryptography.DecryptedText) }
        });
        private FileCrypter fileCrypter;

        public FileCrypterTest() {
            fileCrypter = new FileCrypter(mock_Cryptography, mockFileSystem);
        }

        [TestMethod]
        public void TestFileEncryption()
        {
            fileCrypter.Encrypt("password", @"C:\input.txt", @"C:\output\");
            Assert.IsTrue(mockFileSystem.FileExists(@"C:\output\input.txt.fenc"));
            string filename = "input.txt";
            var fileNameBytes = Encoding.ASCII.GetBytes(filename);
            var fileContentWithFileName = BitConverter.GetBytes(fileNameBytes.Length);
            fileContentWithFileName = fileContentWithFileName.Concat(fileNameBytes).ToArray();
            fileContentWithFileName = fileContentWithFileName.Concat(Mock_Cryptography.DecryptedBytes).ToArray();
            mock_Cryptography.FileContent = fileContentWithFileName;
            fileCrypter.Decrypt("password", @"C:\output\input.txt.fenc", @"C:\output");
            Assert.IsTrue(mockFileSystem.FileExists(@"C:\output\input.txt"));
            var filecontent = mockFileSystem.File.ReadAllBytes(@"C:\output\input.txt");
            CollectionAssert.AreEqual(filecontent, Mock_Cryptography.DecryptedBytes);
        }
    }
}
