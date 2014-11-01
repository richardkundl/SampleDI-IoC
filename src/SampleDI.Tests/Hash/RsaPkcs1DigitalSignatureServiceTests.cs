using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleDI.Cryptography;
using SampleDI.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Tests.Hash
{
    [TestClass]
    public class RsaPkcs1DigitalSignatureServiceTests
    {
        [TestMethod]
        public void DigitalSignatureServiceTest()
        {
            var asymmetricService = new RsaAsymmetricCryptographyService();
            var keyPairGenerationResultReceiver = asymmetricService.GenerateAsymmetricKeys(1024);
            var keyPairGenerationResultSender = asymmetricService.GenerateAsymmetricKeys(1024);
            var digitalSignatureService = new RsaPkcs1DigitalSignatureService(new Sha1ManagedHashingService());

            var signatureCreationArgumentsFromSender = new DigitalSignatureCreationArguments()
            {
                Message = "eper málna körte",
                FullKeyForSignature = keyPairGenerationResultSender.FullKeyPairXml,
                PublicKeyForEncryption = keyPairGenerationResultReceiver.PublicKeyOnlyXml
            };

            var signatureCreationResult = digitalSignatureService.Sign(signatureCreationArgumentsFromSender);
            Assert.AreEqual(true, signatureCreationResult.Success);
            var verificationArgumentsFromReceiver = new DigitalSignatureVerificationArguments();
            verificationArgumentsFromReceiver.CipherText = signatureCreationResult.CipherText;
            verificationArgumentsFromReceiver.Signature = signatureCreationResult.Signature;
            verificationArgumentsFromReceiver.PublicKeyForSignatureVerification = keyPairGenerationResultSender.PublicKeyOnlyXml;
            verificationArgumentsFromReceiver.FullKeyForDecryption = keyPairGenerationResultReceiver.FullKeyPairXml;
            var verificationResult = digitalSignatureService.VerifySignature(verificationArgumentsFromReceiver);
            Assert.AreEqual(true, verificationResult.Success);
            Assert.AreEqual(true, verificationResult.SignaturesMatch);
        }
    }
}
