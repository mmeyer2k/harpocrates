# :herb: Harpocrates

(Ancient Greek: [Ἁρποκράτης](https://en.wikipedia.org/wiki/Harpocrates)) was the god of silence, secrets and confidentiality in the Hellenistic religion.

[DOWNLOAD DLL (1.0.0)](https://github.com/mmeyer2k/harpocrates/releases/download/1.0.0/Harpocrates.dll) | [changelog](https://github.com/mmeyer2k/harpocrates/blob/master/changelog.txt)

Many of the .NET encryption code examples to be found online are **flawed**.
Harpocrates protects your data by protecting you from yourself.

## Features
- The most secure options are chosen by default
- Message authenticity is verified before decryption by comparing SHA-256 hashes
- Strongly random initialization vectors are generated
- Optional key hardening to prevent brute force attacks
- Hardening parameter is stream encrypted added to ciphertext
- DoS attacks against the password cost value are prevented by the checksum step
- API consumes and produces only strings

## Using Harpocrates

```csharp
string encrypted = Harpocrates.Engine.Encrypt("secret!", "password");
string plaintext = Harpocrates.Engine.Decrypt(encrypted, "password");
```

Hardening the password with PBKDF2 helps prevent brute force attacks.
Pass a third parameter to specify the number of iterations.
```csharp
string encrypted = Harpocrates.Engine.Encrypt("secret!", "password", 10000);
string plaintext = Harpocrates.Engine.Decrypt(encrypted, "password", 10000);
```
