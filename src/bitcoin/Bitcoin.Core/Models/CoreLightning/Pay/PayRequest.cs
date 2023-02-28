using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class PayRequest
    {
        public string bolt11 { get; set; }
        public int? satoshi { get; set; } //if no amount is specified
        public string label { get; set; } //if reference is supplied
    }


    public class PayResponse
    {
        public string destination { get; set; }
        public string payment_hash { get; set; }
        public float created_at { get; set; }
        public int parts { get; set; }
        public int msatoshi { get; set; }
        public string amount_msat { get; set; }
        public int msatoshi_sent { get; set; }
        public string amount_sent_msat { get; set; }
        public string payment_preimage { get; set; }
        public string status { get; set; }
    }

}
