# :herb: Harpocrates

(Ancient Greek: [Ἁρποκράτης](https://en.wikipedia.org/wiki/Harpocrates)) was the god of silence, secrets and confidentiality in the Hellenistic religion.

[DOWNLOAD LATEST BUILD](https://github.com/mmeyer2k/harpocrates/blob/master/bin/Debug/Harpocrates.dll?raw=true)

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
