using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class DeleteInvoiceRequest
    {
        public string label { get; set; } //same as reference
        public string status { get; set; } //same as reference
        public bool? desconly { get; set; } //deletes the description/naration only but not the invoice
    }


    public class DeleteInvoiceResponse
    {
        public string label { get; set; }
        public string bolt11 { get; set; }
        public string payment_hash { get; set; }
        public int msatoshi { get; set; }
        public string amount_msat { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public int expires_at { get; set; }
    }

}
