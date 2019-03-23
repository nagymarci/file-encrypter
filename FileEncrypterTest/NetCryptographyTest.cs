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
            string pw = "hello";
            byte[] expectedHash = Convert.FromBase64String("tpw+CunAbADjXB6+o/Mi19MtH5/x5oYchlCvcX8PJ24=");
            var result = netCryptography.HashPassword(pw);
            CollectionAssert.Equals(expectedHash, result);
        }

        [TestMethod]
        public void TestPasswordHash02()
        {
            string pw = "ThisIsMyP@s$wOrd!";
            byte[] expectedHash = Convert.FromBase64String("zYmIc+3f6/fVYFttqDV5DEAib324Ji7XlY/3FwEJSBU=");
            var result = netCryptography.HashPassword(pw);
            CollectionAssert.Equals(expectedHash, result);
        }
    }
}
