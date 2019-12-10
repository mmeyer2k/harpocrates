using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Harpocrates
{
    class KeyManager
    {
        string secret;
        byte[] iv;
        UInt32 iterations;

        public KeyManager(string secret, byte[] iv, UInt32 iterations)
        {
            this.secret = secret;
            this.iv = iv;
            this.iterations = iterations;
        }

        public byte[] deriveKeyForCrypto()
        {
            PasswordDeriveBytes password = new PasswordDeriveBytes
            (
                this.secret + "|aes",
                this.iv,
                "SHA256",
                Convert.ToInt32(this.iterations)
            );

            return password.GetBytes(128 / 8);
        }

        public byte[] deriveKeyForHmac()
        {
            PasswordDeriveBytes password = new PasswordDeriveBytes
            (
                this.secret + "|hmac",
                this.iv,
                "SHA256",
                Convert.ToInt32(this.iterations)
            );

            return password.GetBytes(256 / 8);
        }
    }
}
