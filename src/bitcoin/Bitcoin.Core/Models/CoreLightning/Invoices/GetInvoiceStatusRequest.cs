using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning.Invoices
{
    public class GetInvoiceStatusRequest : Request
    {
        public string Address { get; set; }
        public string Label { get; set; }
    }

    public class GetInvoiceStatusResponse : ListInvoiceResponseInvoice
    {
        
    }
}
