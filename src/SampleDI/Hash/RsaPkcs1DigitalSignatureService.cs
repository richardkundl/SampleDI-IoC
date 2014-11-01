using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Hash
{
    public class RsaPkcs1DigitalSignatureService : IEncryptedDigitalSignatureService
    {
        private readonly IHashingService _hashingService;

        public RsaPkcs1DigitalSignatureService(IHashingService hashingService)
        {
            if (hashingService == null)
            {
                throw new ArgumentNullException("Hashing service");
            }

            _hashingService = hashingService;
        }

        public DigitalSignatureCreationResult Sign(DigitalSignatureCreationArguments arguments)
        {
            var res = new DigitalSignatureCreationResult();
            try
            {
                var rsaProviderReceiver = new RSACryptoServiceProvider();
                rsaProviderReceiver.FromXmlString(arguments.PublicKeyForEncryption.ToString());
                var encryptionResult = rsaProviderReceiver.Encrypt(Encoding.UTF8.GetBytes(arguments.Message), false);
                var hashed = _hashingService.Hash(Convert.ToBase64String(encryptionResult));

                var rsaProviderSender = new RSACryptoServiceProvider();
                rsaProviderSender.FromXmlString(arguments.FullKeyForSignature.ToString());
                var signatureFormatter = new RSAPKCS1SignatureFormatter(rsaProviderSender);
                signatureFormatter.SetHashAlgorithm(_hashingService.HashAlgorithmCode());
                var signature = signatureFormatter.CreateSignature(hashed.HashedBytes);

                res.Signature = signature;
                res.CipherText = Convert.ToBase64String(encryptionResult);
                res.Success = true;
            }
            catch (Exception ex)
            {
                res.ExceptionMessage = ex.Message;
            }

            return res;
        }

        public DigitalSignatureVerificationResult VerifySignature(DigitalSignatureVerificationArguments arguments)
        {
            var res = new DigitalSignatureVerificationResult();
            try
            {
                var rsaProviderSender = new RSACryptoServiceProvider();
                rsaProviderSender.FromXmlString(arguments.PublicKeyForSignatureVerification.ToString());
                var deformatter = new RSAPKCS1SignatureDeformatter(rsaProviderSender);
                deformatter.SetHashAlgorithm(_hashingService.HashAlgorithmCode());
                var hashResult = _hashingService.Hash(arguments.CipherText);
                res.SignaturesMatch = deformatter.VerifySignature(hashResult.HashedBytes, arguments.Signature);
                if (res.SignaturesMatch)
                {
                    var rsaProviderReceiver = new RSACryptoServiceProvider();
                    rsaProviderReceiver.FromXmlString(arguments.FullKeyForDecryption.ToString());
                    var decryptedBytes = rsaProviderReceiver.Decrypt(Convert.FromBase64String(arguments.CipherText), false);
                    res.DecodedText = Encoding.UTF8.GetString(decryptedBytes);
                }

                res.Success = true;
            }
            catch (Exception ex)
            {
                res.ExceptionMessage = ex.Message;
            }

            return res;
        }
    }
}
