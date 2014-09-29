using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SampleDI.Cryptography
{
    public class RsaAsymmetricCryptographyService : IAsymmetricCryptographyService
    {
        public AsymmetricKeyPairGenerationResult GenerateAsymmetricKeys(int keySizeInBits)
        {
            try
            {
                var myRSA = new RSACryptoServiceProvider(keySizeInBits);
                var publicKeyXml = XDocument.Parse(myRSA.ToXmlString(false));
                var fullKeyXml = XDocument.Parse(myRSA.ToXmlString(true));
                return new AsymmetricKeyPairGenerationResult(fullKeyXml, publicKeyXml) { Success = true };
            }
            catch (Exception ex)
            {
                return new AsymmetricKeyPairGenerationResult(new XDocument(), new XDocument()) { ExceptionMessage = ex.Message };
            }
        }

        public AsymmetricEncryptionResult Encrypt(AsymmetricEncryptionArguments arguments)
        {
            var encryptionResult = new AsymmetricEncryptionResult();
            try
            {
                var myRSA = new RSACryptoServiceProvider();
                var rsaKeyForEncryption = arguments.PublicKeyForEncryption.ToString();
                myRSA.FromXmlString(rsaKeyForEncryption);
                byte[] data = Encoding.UTF8.GetBytes(arguments.PlainText);
                byte[] cipherText = myRSA.Encrypt(data, false);
                encryptionResult.CipherBytes = cipherText;
                encryptionResult.CipherText = Convert.ToBase64String(cipherText);
                encryptionResult.Success = true;
            }
            catch (Exception ex)
            {
                encryptionResult.ExceptionMessage = ex.Message;
            }

            return encryptionResult;
        }

        public CipherTextDecryptionResult Decrypt(AsymmetricDecryptionArguments arguments)
        {
            var decryptionResult = new CipherTextDecryptionResult();
            try
            {
                var cipher = new RSACryptoServiceProvider();
                cipher.FromXmlString(arguments.FullAsymmetricKey.ToString());
                byte[] original = cipher.Decrypt(Convert.FromBase64String(arguments.CipherText), false);
                decryptionResult.DecodedText = Encoding.UTF8.GetString(original);
                decryptionResult.Success = true;
            }
            catch (Exception ex)
            {
                decryptionResult.ExceptionMessage = ex.Message;
            }

            return decryptionResult;
        }
    }
}