using System.Security.Cryptography;

namespace RC4Cryptography
{
    class RC4CryptoProvider : SymmetricAlgorithm
    {
        int _blockLen;

        public RC4CryptoProvider(int BlockLenght = 8)
        {
            _blockLen = BlockLenght;
            this.ModeValue = CipherMode.CBC;
        }

        public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
        {
            return new RC4CryptoTransform(rgbKey, _blockLen);
        }

        public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
        {
            return new RC4CryptoTransform(rgbKey, _blockLen);
        }

        public override void GenerateIV()
        {
            throw new CryptographicException("RC4 don't support IV");
        }

        public override void GenerateKey()
        {
            var rnd = new RNGCryptoServiceProvider();
            KeyValue = new byte[KeySizeValue];
            rnd.GetBytes(KeyValue);
        }
    }
}
