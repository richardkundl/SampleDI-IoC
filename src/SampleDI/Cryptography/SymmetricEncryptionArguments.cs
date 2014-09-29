using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Cryptography
{
    public class SymmetricEncryptionArguments : CommonSymmetricCryptoArguments
    {
        public string PlainText { get; set; }
    }
}
