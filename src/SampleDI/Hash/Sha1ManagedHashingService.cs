using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Hash
{
    public class Sha1ManagedHashingService : IHashingService
    {
        public HashResult Hash(string message)
        {
            var hashResult = new HashResult();
            try
            {
                var sha1 = new SHA1Managed();
                var hashed = sha1.ComputeHash(Encoding.UTF8.GetBytes(message));
                var hex = Convert.ToBase64String(hashed);
                hashResult.HashedBytes = hashed;
                hashResult.HexValueOfHash = hex;
            }
            catch (Exception ex)
            {
                hashResult.ExceptionMessage = ex.Message;
            }

            return hashResult;
        }

        public string HashAlgorithmCode()
        {
            return "SHA1";
        }
    }
}
