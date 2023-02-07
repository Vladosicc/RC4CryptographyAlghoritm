# RC4 Cryptography alghoritm

***

Description of the algorithm - https://en.wikipedia.org/wiki/RC4

***

The class ``RC4CryptoProvider`` implements the abstract class ``System.Security.Cryptography.SymmetricAlgorithm``

The class ``RC4CryptoTransform`` implements the interface ``System.Security.Cryptography.ICryptoTransform``

***

**Example of use**

```
using RC4Cryptography;

//...

byte[] source;
byte[] key;

//output array
byte[] encrypted;

using (var RC4 = new RC4CryptoProvider())
{
    var encryptor = RC4.CreateEncryptor(key, null);

    // Create the streams used for encryption. 
    using (var msEncrypt = new MemoryStream())
    {
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            csEncrypt.Write(source, 0, source.Length);
            encrypted = msEncrypt.ToArray();
        }
    }
}
```

**Or**

```
using RC4Cryptography;

//...

byte[] source;
byte[] key;

var rc4 = new RC4Cipher();
var encrypted = rc4.Encrypt(key, source);
```
