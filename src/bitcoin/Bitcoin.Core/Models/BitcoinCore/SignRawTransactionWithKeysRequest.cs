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
        public SignRawTransactionWithKeysResponse()
        {
            errors = new List<SignRawTransactionWithKeysResponseError>();
        }

        public string hex { get; set; }
        public bool complete { get; set; } 
        public List<SignRawTransactionWithKeysResponseError> errors { get; set; }
    } 

    public class SignRawTransactionWithKeysResponseError
    {
        public string txid { get; set; }
        public int vout { get; set; }
        public object[] witness { get; set; }
        public string scriptSig { get; set; }
        public long sequence { get; set; }
        public string error { get; set; }
    }

}
