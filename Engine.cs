using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Harpocrates
{
    public class Engine
    {
        public static string Encrypt
        (
            string plainText,
            string secret,
            UInt32 iterations = 1
        )
        {
            // Create a properly sized initialization vector
            byte[] initVectorBytes = new byte[16];
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            rng.GetBytes(initVectorBytes);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Initialize the key manager
            KeyManager keyManager = new KeyManager(secret, initVectorBytes, iterations);

            // Derive the crypto key
            byte[] keyBytes = keyManager.deriveKeyForCrypto();

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor
            (
                keyBytes,
                initVectorBytes
            );

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = new CryptoStream
            (
                memoryStream,
                encryptor,
                CryptoStreamMode.Write
            );

            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Create a hmac object based on our secret key
            keyBytes = keyManager.deriveKeyForHmac();
            HMACSHA256 hmac = new HMACSHA256(keyBytes);

            // Append iv to front of message
            cipherTextBytes = Combine(initVectorBytes, cipherTextBytes);

            // Calculate checksum of iv + message
            byte[] checksum = hmac.ComputeHash(cipherTextBytes);

            // Add checksum to very front of ciphertext message
            cipherTextBytes = Combine(checksum, cipherTextBytes);

            // Convert encrypted data into a base64-encoded string and return
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt
        (
            string cipherText,
            string secret,
            UInt32 iterations = 1
        )
        {
            // Convert our ciphertext into a byte array
            byte[] rawBytes = Convert.FromBase64String(cipherText);

            // Separate the parts of the cipher text
            byte[] checksum = rawBytes.Take(32).ToArray();
            byte[] initVectorBytes = rawBytes.Skip(32).Take(16).ToArray();
            byte[] cipherTextBytes = rawBytes.Skip(48).ToArray();

            // Initialize the key manager
            KeyManager keyManager = new KeyManager(secret, initVectorBytes, iterations);

            // Create a hmac object based on our secret key
            byte[] keyBytes = keyManager.deriveKeyForHmac();
            HMACSHA256 hmac = new HMACSHA256(keyBytes);

            // Calculate the hash sent against the rest of the payload by splitting the
            // cipher text after the 32nd byte
            byte[] checksumCalculated = hmac.ComputeHash(rawBytes.Skip(32).ToArray());

            if (checksum.SequenceEqual(checksumCalculated) == false)
            {
                throw new Exception("Ciphertext checksum does not match calculated value");
            }

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Derive the crypto key
            keyBytes = keyManager.deriveKeyForCrypto();

            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor
            (
                keyBytes,
                initVectorBytes
            );

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Define cryptographic stream (always use Read mode for encryption).
            CryptoStream cryptoStream = new CryptoStream
            (
                memoryStream,
                decryptor,
                CryptoStreamMode.Read
            );

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read
            (
                plainTextBytes,
                0,
                plainTextBytes.Length
            );

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = Encoding.UTF8.GetString
            (
                plainTextBytes,
                0,
                decryptedByteCount
            );

            // Return decrypted string.   
            return plainText;
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }
    }
}
