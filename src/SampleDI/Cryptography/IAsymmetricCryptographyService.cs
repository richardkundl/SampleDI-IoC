using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Cryptography
{
    public interface IAsymmetricCryptographyService
    {
        AsymmetricKeyPairGenerationResult GenerateAsymmetricKeys(int keySizeInBits);
        AsymmetricEncryptionResult Encrypt(AsymmetricEncryptionArguments arguments);
        CipherTextDecryptionResult Decrypt(AsymmetricDecryptionArguments arguments);
    }
}
