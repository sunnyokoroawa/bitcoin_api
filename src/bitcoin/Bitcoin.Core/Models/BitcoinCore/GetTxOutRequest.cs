using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetTxOutRequest
    {
        public string TxId { get; set; }
        public int n { get; set; }
        public bool include_mempool { get; set; }
    }


    public class GetTxOutResponse
    {
        public string bestblock { get; set; }
        public int confirmations { get; set; }
        public float value { get; set; }
        public GetTxOutResponseScriptpubkey scriptPubKey { get; set; }
        public bool coinbase { get; set; }
    }

    public class GetTxOutResponseScriptpubkey
    {
        public string asm { get; set; }
        public string hex { get; set; }
        public string address { get; set; }
        public string type { get; set; }
    }

}
