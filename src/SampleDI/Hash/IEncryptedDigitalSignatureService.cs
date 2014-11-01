using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Hash
{
    public interface IEncryptedDigitalSignatureService
    {
        DigitalSignatureCreationResult Sign(DigitalSignatureCreationArguments arguments);
        DigitalSignatureVerificationResult VerifySignature(DigitalSignatureVerificationArguments arguments);
    }
}
