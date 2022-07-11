using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class DecodeRawTransactionRequest
    {
        public string Hex { get; set; }
    }
     
    public class DecodeRawTransactionResponse
    {
        public string txid { get; set; }
        public string hash { get; set; }
        public int version { get; set; }
        public int size { get; set; }
        public int vsize { get; set; }
        public int weight { get; set; }
        public int locktime { get; set; }
        public List<DecodeRawTransactionResponseVin> vin { get; set; }
        public List<DecodeRawTransactionResponseVout> vout { get; set; }
    }

    public class DecodeRawTransactionResponseVin
    {
        public string txid { get; set; }
        public int vout { get; set; }
        public DecodeRawTransactionResponseScriptsig scriptSig { get; set; }
        public long sequence { get; set; }
    }

    public class DecodeRawTransactionResponseScriptsig
    {
        public string asm { get; set; }
        public string hex { get; set; }
    }

    public class DecodeRawTransactionResponseVout
    {
        public float value { get; set; }
        public int n { get; set; }
        public DecodeRawTransactionResponseScriptpubkey scriptPubKey { get; set; }
    }

    public class DecodeRawTransactionResponseScriptpubkey
    {
        public string asm { get; set; }
        public string hex { get; set; }
        public string address { get; set; }
        public string type { get; set; }
    }

}
