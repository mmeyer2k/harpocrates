using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;

namespace Harpocrates.Tests
{
    [TestClass()]
    public class EngineTests
    {
        [TestMethod()]
        public void EncryptTest()
        {
            string a = "AAAA";
            string b = Harpocrates.Engine.Encrypt(a, "BBBB");
            string c = Harpocrates.Engine.Decrypt(b, "BBBB");

            Assert.AreEqual(a, c);
        }

        [TestMethod()]
        public void EncryptTestHigherIterations()
        {
            string a = "AAAA";
            string b = Harpocrates.Engine.Encrypt(a, "BBBB", 10000);
            string c = Harpocrates.Engine.Decrypt(b, "BBBB", 10000);

            Assert.AreEqual(a, c);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.Exception))]
        public void EncryptTestIterationMismatch()
        {
            string a = "AAAA";
            string b = Harpocrates.Engine.Encrypt(a, "BBBB", 10001);
            string c = Harpocrates.Engine.Decrypt(b, "BBBB", 10000);

            Assert.AreEqual(a, c);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.Exception))]
        public void EncryptTestBadCiphertext()
        {
            string a = "AAAA";
            string c = Harpocrates.Engine.Decrypt(a, "BBBB", 10000);

            Assert.AreEqual(a, c);
        }

        [TestMethod()]
        public void EncryptTestRandomInput()
        {
            byte[] random = new byte[4096];
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            rng.GetBytes(random);

            string a = random.ToString();
            string b = Harpocrates.Engine.Encrypt(a, "BBBB");
            string c = Harpocrates.Engine.Decrypt(b, "BBBB");

            Assert.AreEqual(a, c);
        }
    }
}