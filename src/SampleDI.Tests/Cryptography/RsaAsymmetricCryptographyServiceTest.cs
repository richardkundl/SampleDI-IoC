using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleDI.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Tests.Cryptography
{
    [TestClass]
    public class RsaAsymmetricCryptographyServiceTest
    {
        [TestMethod]
        public void RsaAsymmetricCryptographyServiceEncryptDecryptTest()
        {
            var asymmetricSevice = new RsaAsymmetricCryptographyService();
            var keyPairGenerationResult = asymmetricSevice.GenerateAsymmetricKeys(1024);
            Assert.AreEqual(true, keyPairGenerationResult.Success);

            var encryptionArgs = new AsymmetricEncryptionArguments
            {
                PlainText = "Text to be encrypted",
                PublicKeyForEncryption = keyPairGenerationResult.PublicKeyOnlyXml
            };

            var encryptionResult = asymmetricSevice.Encrypt(encryptionArgs);
            Assert.AreEqual(true, encryptionResult.Success);

            var decryptionArguments = new AsymmetricDecryptionArguments()
            {
                CipherText = encryptionResult.CipherText,
                FullAsymmetricKey = keyPairGenerationResult.FullKeyPairXml
            };

            var decryptionResult = asymmetricSevice.Decrypt(decryptionArguments);
            Assert.AreEqual(true, decryptionResult.Success);
        }
    }
}
