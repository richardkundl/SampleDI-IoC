using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SampleDI.Cryptography
{
    public class AsymmetricEncryptionArguments
    {
        public XDocument PublicKeyForEncryption { get; set; }
        public string PlainText { get; set; }
    }
}
