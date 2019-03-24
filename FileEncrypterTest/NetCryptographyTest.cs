using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileEncrypter;
using System.Text;

namespace FileEncrypterTest
{
    [TestClass]
    public class NetCryptographyTest
    {
        private NetCryptography netCryptography = new NetCryptography();

        [TestMethod]
        public void TestPasswordHash01()
        {
            string pw = "password";
            byte[] expectedHash = Convert.FromBase64String("cRp0Qw+CnmJhn2Spvf0e7DC60E4Q9rCGwAsbhZn8v9w=");
            var result = netCryptography.HashPassword(pw);
            CollectionAssert.AreEqual(expectedHash, result);
        }

        [TestMethod]
        public void TestPasswordHash02()
        {
            string pw = "ThisIsMyP@s$wOrd!";
            byte[] expectedHash = Convert.FromBase64String("hjiKlYOLVJbkM4yeGfZSQLbxz8RVFggYHVX6TXCx8ww=");
            var result = netCryptography.HashPassword(pw);
            var r = Convert.ToBase64String(result);
            CollectionAssert.AreEqual(expectedHash, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPasswordHash_EmptyInput()
        {
            string pw = "";
            var result = netCryptography.HashPassword(pw);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPasswordHash_NullInput()
        {
            string pw = null;
            var result = netCryptography.HashPassword(pw);
        }

        [TestMethod]
        public void TestEncryption()
        {
            string inputString = "Hello world!";
            byte[] encryptionKey = Convert.FromBase64String("hjiKlYOLVJbkM4yeGfZSQLbxz8RVFggYHVX6TXCx8ww=");
            byte[] input = ASCIIEncoding.ASCII.GetBytes(inputString);
            byte[] encryptedInput = netCryptography.Encrypt(encryptionKey, input);
            byte[] decryptedInput = netCryptography.Decrypt(encryptionKey, encryptedInput);
            string output = ASCIIEncoding.ASCII.GetString(decryptedInput);

            CollectionAssert.AreEqual(input, decryptedInput);
            Assert.AreEqual(inputString, output);
        }
    }
}
