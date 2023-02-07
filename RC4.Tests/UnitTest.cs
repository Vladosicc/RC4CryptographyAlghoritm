using RC4Cryptography;
using System;
using System.Text;
using Xunit;

namespace RC4.Tests
{
    public class UnitTest
    {
        /// <summary>
        /// Key for encrypt (256 lenght)
        /// </summary>
        public static byte[] _key = new byte[]
        {
               196, 67,  67,  4,   23,
               96,  161, 99,  185, 14,
               119, 212, 190, 170, 78,
               203, 173, 228, 17,  230,
               53,  96,  7,   113, 29,
               140, 63,  66,  16,  106,
               194, 202, 65,  78,  24,
               231, 133, 121, 65,  121,
               74,  153, 254, 102, 201,
               36,  58,  188, 50,  223,
               253, 69,  64,  138, 113,
               35,  43,  207, 232, 177,
               37,  126, 199, 132, 15,
               32,  22,  232, 2,   145,
               154, 105, 17,  106, 19,
               55,  102, 221, 6,   97,
               1,   213, 231, 72,  121,
               212, 10,  48,  225, 151,
               73,  242, 35,  56,  55,
               241, 130, 122, 107, 101,
               12,  146, 247, 6,   212,
               197, 237, 141, 86,  231,
               170, 254, 239, 198, 210,
               136, 130, 234, 70,  36,
               97,  34,  255, 97,  183,
               30,  193, 90,  255, 175,
               170, 213, 224, 162, 206,
               22,  44,  105, 194, 182,
               60,  173, 177, 157, 51,
               157, 108, 129, 216, 187,
               102, 145, 92,  231, 94,
               209, 46,  165, 71,  128,
               130, 1,   252, 111, 252,
               190, 42,  82,  26,  187,
               255, 13,  119, 179, 35,
               170, 34,  101, 188, 13,
               203, 200, 73,  167, 44,
               77,  102, 132, 126, 40,
               206, 186, 44,  23,  168,
               197, 121, 41,  181, 147,
               132, 197, 1,   240, 206,
               66,  5,   213, 189, 169,
               153, 27,  15,  205, 46,
               216, 222, 130, 64,  174,
               169, 240, 54,  227, 67,
               119, 115, 224, 217, 244,
               206, 28,  138, 176, 39,
               194, 18,  215, 87,  89,
               136, 63,  176, 82,  128,
               28,  98,  165, 12,  227,
               196, 217, 69,  252, 20,  175
        };

        /// <summary>
        /// Сase for encrypt (byte array)
        /// </summary>
        static readonly public byte[] _sourceExample = new byte[]
        {
            152, 86,  89,  188, 224,
            183, 56, 113, 18, 26,
            61,  153, 51,  66, 106,
            187 ,252, 4, 217 ,230
        };

        /// <summary>
        /// Encrypted case (byte array)
        /// </summary>
        static public readonly byte[] _encryptedExample = new byte[]
        {
            81, 157, 183, 206, 30, 135, 124, 198, 85, 85, 166, 141, 159, 135, 121, 54, 131, 220, 148, 214,
        };


        //[Theory]
        //[InlineData(new object[] {"s", "e" })]
        //public void EncryptString(string source, string enc)
        //{
        //    var rc4 = new RC4Cipher();
        //    var encrypted = rc4.Encrypt(_key, Encoding.Unicode.GetBytes(source));

        //    Assert.Equal(enc, Encoding.Unicode.GetString(encrypted));
        //}
        
        [Fact]
        public void EncryptArray()
        {
            var rc4 = new RC4Cipher();
            var encrypted = rc4.Encrypt(_key, _sourceExample);

            Assert.Equal(_encryptedExample, encrypted);
        }

        [Fact]
        public void EncryptZeroLenght()
        {
            var rc4 = new RC4Cipher();

            var encrypted = rc4.Encrypt(_key, new byte[0]);
            Assert.Equal(new byte[0], encrypted);
        }

        [Fact]
        public void EncryptNull()
        {
            try
            {
                var rc4 = new RC4Cipher();

                var encrypted = rc4.Encrypt(_key, null);

                Assert.Fail("Зашифрован null");
            }
            catch (Exception ex)
            {
                Assert.IsType<System.NullReferenceException>(ex);
            }
        }

        [Fact]
        public void DecryptArray()
        {
            var rc4 = new RC4Cipher();
            var decrypted = rc4.Decrypt(_key, _encryptedExample);

            Assert.Equal(_sourceExample, decrypted);
        }

        [Fact]
        public void EncryptWithoutKey()
        {
            try
            {
                var rc4 = new RC4Cipher();
                var decrypted = rc4.Encrypt(new byte[0], _encryptedExample);

                Assert.Fail("Ключ с длинной 0");
            }
            catch (Exception ex)
            {
                Assert.IsType<System.DivideByZeroException>(ex);
            }
        }

        [Fact]
        public void EncryptWithNullKey()
        {
            try
            {
                var rc4 = new RC4Cipher();
                var decrypted = rc4.Encrypt(null, _encryptedExample);

                Assert.Fail("Ключ = null");
            }
            catch (Exception ex)
            {
                Assert.IsType<System.NullReferenceException>(ex);
            }
        }
    }
}