# :herb: Harpocrates

(Ancient Greek: [Ἁρποκράτης](https://en.wikipedia.org/wiki/Harpocrates)) was the god of silence, secrets and confidentiality in the Hellenistic religion.

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

To install Harpocrates in your project import the DLL stored in `bin/Debug/`
