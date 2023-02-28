using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class ListInvoicesRequest
    {

    }


    public class ListInvoicesResponse
    {
        public ListInvoicesResponse()
        {
            invoices = new List<ListInvoiceResponseInvoice>();
        }

        public List<ListInvoiceResponseInvoice> invoices { get; set; }
    }

    public class ListInvoiceResponseInvoice
    {
        public string label { get; set; }
        public string bolt11 { get; set; }
        public string bolt12 { get; set; } //static invoices
        public string payment_hash { get; set; }
        public long msatoshi { get; set; }
        public string amount_msat { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public long expires_at { get; set; }
    }

}
