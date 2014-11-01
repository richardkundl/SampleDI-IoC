using SampleDI.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Hash
{
    public class DigitalSignatureCreationResult : ResponseBase
    {
        public byte[] Signature { get; set; }
        public string CipherText { get; set; }
    }
}
