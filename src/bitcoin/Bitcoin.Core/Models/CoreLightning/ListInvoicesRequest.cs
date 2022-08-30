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
            invoices = new List<ListInvoicesResponseInvoice>();
        }

        public List<ListInvoicesResponseInvoice> invoices { get; set; }
    }

    public class ListInvoicesResponseInvoice
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
