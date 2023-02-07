using System.Security.Cryptography;

namespace RC4Cryptography
{
    class RC4CryptoTransform : ICryptoTransform
    {
        //Block length in bits
        private int _sBlockLenght;
        public int SBlockLenght { get { return _sBlockLenght; } }

        private byte[] _rgbKey;

        private byte[] _sBlock;

        private int _rndI = 0;
        private int _rndJ = 0;

        public RC4CryptoTransform(byte[] rgbKey, int BlockLenght)
        {
            _sBlockLenght = BlockLenght;
            _rgbKey = rgbKey;

            //key-scheduling algorithm
            var blockSize = (int)Math.Pow(2, _sBlockLenght);
            _sBlock = new byte[blockSize];
            int keyLength = rgbKey.Length;

            for (int i = 0; i < blockSize; i++)
            {
                _sBlock[i] = (byte)i;
            }

            int j = 0;
            for (int i = 0; i < blockSize; i++)
            {
                j = (j + _sBlock[i] + rgbKey[i % keyLength]) % blockSize;
                ArrayExtension.Swap(_sBlock, i, j);
            }
        }

        /// <summary>
        /// pseudo-random generation algorithm, PRGA
        /// </summary>
        /// <returns></returns>
        private byte getRandom()
        {
            var blockSize = (int)Math.Pow(2, _sBlockLenght);

            _rndI = (_rndI + 1) % blockSize;
            _rndJ = (_rndJ + _sBlock[_rndI]) % blockSize;

            ArrayExtension.Swap(_sBlock, _rndI, _rndJ);

            return _sBlock[(_sBlock[_rndI] + _sBlock[_rndJ]) % blockSize];
        }

        public void Dispose()
        {
            Array.Clear(_rgbKey, 0, _rgbKey.Length);
            Array.Clear(_sBlock, 0, _sBlock.Length);
        }

        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            for (long i = inputOffset; i < inputOffset + inputCount; i++)
            {
                outputBuffer[outputOffset + i - inputOffset] = (byte)(inputBuffer[i] ^ getRandom());
            }
            return inputCount;
        }

        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            var encryptedData = new byte[inputCount];
            TransformBlock(inputBuffer, inputOffset, inputCount, encryptedData, 0);
            return encryptedData;
        }

        public bool CanReuseTransform { get { return false; } }

        public bool CanTransformMultipleBlocks { get { return false; } }

        public int InputBlockSize { get { return SBlockLenght / 8; } }

        public int OutputBlockSize { get { return InputBlockSize; } }

        private static class ArrayExtension
        {
            public static void Swap(byte[] array, int index1, int index2)
            {
                var tmp = array[index1];
                array[index1] = array[index2];
                array[index2] = tmp;
            }
        }
    }
}
