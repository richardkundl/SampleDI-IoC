using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleDI.Cryptography;

namespace SampleDI.Tests
{
    [TestClass]
    public class RijndaelManagedSymmetricEncryptionServiceTest
    {
        [TestMethod]
        public void RijndaelManagedSymmetricEncryptionServiceEncryptDecryptTest()
        {
            var symmetricService = new RijndaelManagedSymmetricEncryptionService();
            var validKey = symmetricService.GenerateSymmetricKey(128);
            Assert.AreEqual(true, validKey.Success);

            var encryptionArgs = new SymmetricEncryptionArguments
            {
                BlockSize = 128,
                CipherMode = System.Security.Cryptography.CipherMode.CBC,
                KeySize = 256,
                PaddingMode = System.Security.Cryptography.PaddingMode.ISO10126,
                PlainText = "Text to be encoded",
                SymmetricPublicKey = validKey.SymmetricKey
            };

            var encryptionResult = symmetricService.Encrypt(encryptionArgs);
            Assert.AreEqual(true, encryptionResult.Success);

            var decryptionArgs = new SymmetricDecryptionArguments
            {
                BlockSize = encryptionArgs.BlockSize,
                CipherMode = encryptionArgs.CipherMode,
                CipherTextBase64Encoded = encryptionResult.CipherText,
                InitialisationVectorBase64Encoded = encryptionResult.InitialisationVector,
                KeySize = encryptionArgs.KeySize,
                PaddingMode = encryptionArgs.PaddingMode,
                SymmetricPublicKey = validKey.SymmetricKey
            };

            var decryptionResult = symmetricService.Decrypt(decryptionArgs);
            Assert.AreEqual(true, decryptionResult.Success);
        }
    }
}
