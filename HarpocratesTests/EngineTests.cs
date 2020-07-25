using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;

namespace Harpocrates.Tests
{
    [TestClass()]
    public class EngineTests
    {
        string a = "AAAA";
        string password = "PaSsWoRdPaSsWoRdPaSsWoRdPaSsWoRd";

        [TestMethod()]
        public void EncryptTest()
        {
            string b = Harpocrates.Engine.Encrypt(a, password);
            string c = Harpocrates.Engine.Decrypt(b, password);

            Assert.AreEqual(a, c);
        }

        [TestMethod()]
        public void EncryptTestHigherIterations()
        {
            string b = Harpocrates.Engine.Encrypt(a, password, 10000);
            string c = Harpocrates.Engine.Decrypt(b, password, 10000);

            Assert.AreEqual(a, c);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.Exception))]
        public void EncryptTestIterationMismatch()
        {
            string b = Harpocrates.Engine.Encrypt(a, password, 10001);
            string c = Harpocrates.Engine.Decrypt(b, password, 10000);

            Assert.AreEqual(a, c);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.Exception))]
        public void EncryptTestBadCiphertext()
        {
            string c = Harpocrates.Engine.Decrypt(a, password, 10000);

            Assert.AreEqual(a, c);
        }

        [TestMethod()]
        public void EncryptTestRandomInput()
        {
            byte[] random = new byte[4096];
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            rng.GetBytes(random);

            string a = random.ToString();
            string b = Harpocrates.Engine.Encrypt(a, password);
            string c = Harpocrates.Engine.Decrypt(b, password);

            Assert.AreEqual(a, c);
        }

        [TestMethod()]
        public void KnownVectorTest()
        {
            string vector = "rbEg3TXw1Hl3uHFKY+pGhW7VYziGfoHYsimuUcfBdW/jXT04qogqzfy7iez1CN4S19aI47g/XeiHad6Q3VE0dA==";

            string d = Harpocrates.Engine.Decrypt(vector, password);

            Assert.AreEqual(d, a);
        }
    }
}