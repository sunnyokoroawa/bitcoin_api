using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetRawTransactionAsJSONRequest
    {
        public string TxId { get; set; } 
    }


    public class GetRawTransactionAsJSONResponse
    {
        public GetRawTransactionAsJSONResponse()
        {
            vin = new List<GetRawJSONTransactionResponseVin>();
            vout = new List<GetRawJSONTransactionResponseVout>();
        }

        public string txid { get; set; }
        public string hash { get; set; }
        public int version { get; set; }
        public int size { get; set; }
        public int vsize { get; set; }
        public int weight { get; set; }
        public int locktime { get; set; }
        public List<GetRawJSONTransactionResponseVin> vin { get; set; }
        public List<GetRawJSONTransactionResponseVout> vout { get; set; }
        public string hex { get; set; }
        public string blockhash { get; set; }
        public int confirmations { get; set; }
        public int time { get; set; }
        public int blocktime { get; set; }
    }

    public class GetRawJSONTransactionResponseVin
    {
        public GetRawJSONTransactionResponseVin()
        {
            txinwitness = new List<string>();
        }
        public string txid { get; set; }
        public int vout { get; set; }
        public GetRawJSONTransactionResponseScriptsig scriptSig { get; set; }
        public List<string> txinwitness { get; set; }
        public long sequence { get; set; }
    }

    public class GetRawJSONTransactionResponseScriptsig
    {
        public string asm { get; set; }
        public string hex { get; set; }
    }

    public class GetRawJSONTransactionResponseVout
    {
        public float value { get; set; }
        public int n { get; set; }
        public GetRawJSONTransactionResponseScriptpubkey scriptPubKey { get; set; }
    }

    public class GetRawJSONTransactionResponseScriptpubkey
    {
        public string asm { get; set; }
        public string hex { get; set; }
        public string address { get; set; }
        public string type { get; set; }
    }

}
