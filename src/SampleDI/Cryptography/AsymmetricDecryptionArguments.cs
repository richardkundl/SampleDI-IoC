using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SampleDI.Cryptography
{
    public class AsymmetricDecryptionArguments
    {
        public XDocument FullAsymmetricKey { get; set; }
        public string CipherText { get; set; }
    }
}
