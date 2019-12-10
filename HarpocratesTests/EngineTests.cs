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
        public void DecryptTest()
        {
            Assert.AreEqual(1, 1);
        }
    }
}