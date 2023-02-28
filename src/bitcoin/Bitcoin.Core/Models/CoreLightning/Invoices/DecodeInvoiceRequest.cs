using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class DecodeInvoiceRequest
    {
        public string Invoice { get; set; }
    }


    public class DecodeInvoiceResponse
    {
        public string type { get; set; }
        public string currency { get; set; }
        public long created_at { get; set; }
        public long expiry { get; set; }
        public string payee { get; set; }
        public int msatoshi { get; set; }
        public string amount_msat { get; set; }
        public string description { get; set; }
        public int min_final_cltv_expiry { get; set; }
        public string payment_secret { get; set; }
        public string features { get; set; }
        public string payment_hash { get; set; }
        public string signature { get; set; }
        public bool valid { get; set; }
    }

}
