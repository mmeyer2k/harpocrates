using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void DecryptTest()
        {
            Assert.AreEqual(1, 1);
        }
    }
}