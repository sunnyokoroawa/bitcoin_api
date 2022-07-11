using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class SignRawTransactionWithKeysRequest
    {
        public string Hex { get; set; }
        public List<string> SendAddressPrivateKeys { get; set; }
    }


    public class SignRawTransactionWithKeysResponse
    {
        public string hex { get; set; }
        public bool complete { get; set; }
    }

}
