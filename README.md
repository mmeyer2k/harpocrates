# :herb: Harpocrates

(Ancient Greek: [Ἁρποκράτης](https://en.wikipedia.org/wiki/Harpocrates)) was the god of silence, secrets and confidentiality in the Hellenistic religion.

[DOWNLOAD LATEST BUILD](https://github.com/mmeyer2k/harpocrates/blob/master/bin/Debug/Harpocrates.dll?raw=true)

Harpocrates protects your data by protecting you from yourself. 
The most secure options are chosen by default.
HMAC validation checksum and initialization vector are packed with the ciphertext for easy storage of a single string.
Even the password hardening cost is encrypted and added to the ciphertext.
This API consumes and returns only strings.

**MANY .NET ENCRYPTION CODE EXAMPLES YOU FIND ONLINE ARE FLAWED**

A basic usage of Harpocrates can be seen below.

```csharp
string Password = "a password to en/decrypt with";

string Ciphertext = Harpocrates.Engine.Encrypt("some data to encrypt", Password);
string OriginalText = Harpocrates.Engine.Decrypt(Ciphertext, Password);
```

Hardening the password with PBKDF2 helps prevent brute force attacks.
The value of the cost parameter is encrypted and stored with the ciphertext so it does not need to be referenced during decryption.
```csharp
string Ciphertext = Harpocrates.Engine.Encrypt(Data, Password, 999999);
string OriginalText = Harpocrates.Engine.Decrypt(Ciphertext, Password);
```
