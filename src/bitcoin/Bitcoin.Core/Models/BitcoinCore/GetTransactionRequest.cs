using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetTransactionRequest
    {
        public string TxId { get; set; }
    }


    public class GetTransactionResponse
    {
        public GetTransactionResponse()
        {
            details = new List<GetTransactionResponseDetail>();
        }

        public float amount { get; set; }
        public float fee { get; set; }
        public int confirmations { get; set; }
        public string blockhash { get; set; }
        public int blockheight { get; set; }
        public int blockindex { get; set; }
        public int blocktime { get; set; }
        public string txid { get; set; }
        public object[] walletconflicts { get; set; }
        public int time { get; set; }
        public int timereceived { get; set; }
        public string bip125replaceable { get; set; }
        public List<GetTransactionResponseDetail>  details { get; set; }
        public string hex { get; set; }
    }

    public class GetTransactionResponseDetail
    {
        public string address { get; set; }
        public string category { get; set; }
        public float amount { get; set; }
        public string label { get; set; }
        public int vout { get; set; }
        public float fee { get; set; }
        public bool abandoned { get; set; }
    }

}
