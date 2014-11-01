using SampleDI.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Hash
{
    public class HashResult : ResponseBase
    {
        public string HexValueOfHash { get; set; }
        public byte[] HashedBytes { get; set; }
    }
}
