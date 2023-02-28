using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetMempoolTransactionRequest
    {
        public string TxId { get; set; }
    }


    public class GetMempoolTransactionResponse
    {
        public GetMempoolTransactionResponse()
        {
            vin = new List<Vin>();
            vout = new List<Vout>();
        }

        public string txid { get; set; }
        public long version { get; set; }
        public long locktime { get; set; }
        public List<Vin> vin { get; set; }
        public List<Vout> vout { get; set; }
        public int size { get; set; }
        public int weight { get; set; }
        public int fee { get; set; }
        public Status status { get; set; }
    }

    public class Status
    {
        public bool confirmed { get; set; }
    }

    public class Vin
    {
        public string txid { get; set; }
        public long vout { get; set; }
        public Prevout prevout { get; set; }
        public string scriptsig { get; set; }
        public string scriptsig_asm { get; set; }
        public List<string> witness { get; set; }
        public bool is_coinbase { get; set; }
        public long sequence { get; set; }
    }

    public class Prevout
    {
        public string scriptpubkey { get; set; }
        public string scriptpubkey_asm { get; set; }
        public string scriptpubkey_type { get; set; }
        public string scriptpubkey_address { get; set; }
        public long value { get; set; }
    }

    public class Vout
    {
        public string scriptpubkey { get; set; }
        public string scriptpubkey_asm { get; set; }
        public string scriptpubkey_type { get; set; }
        public string scriptpubkey_address { get; set; }
        public long value { get; set; }
    }

}
