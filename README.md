# :herb: harpocrates

(Ancient Greek: Ἁρποκράτης) was the god of silence, secrets and confidentiality in the Hellenistic religion.


Harpocrates protects your data by protecting you from yourself. 
The most secure options are chosen by default.
HMAC validation checksum and initialization vector are packed with the ciphertext for easy storage of a single string.
Even the password hardening cost is encrypted and added to the ciphertext.

```csharp
string Ciphertext = Harpocrates.Engine.Encrypt("some data to encrypt", "a password to encrypt with");
```
