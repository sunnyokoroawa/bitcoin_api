using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class ListUnspentRequest
    {
        public ListUnspentRequest()
        {
            Addresses = new List<string>();
        }

        public int NumberOfConfirmations { get; set; }
        //public string Address { get; set; }
        public List<string> Addresses { get; set; }
    }

     

    public class ListUnspentResponse
    {
        public string txid { get; set; }
        public int vout { get; set; }
        public string address { get; set; }
        public string scriptPubKey { get; set; }
        public decimal amount { get; set; }
        public int confirmations { get; set; }
        public bool spendable { get; set; }
        public bool solvable { get; set; }
        public string desc { get; set; }
        public bool safe { get; set; }
    }

}
