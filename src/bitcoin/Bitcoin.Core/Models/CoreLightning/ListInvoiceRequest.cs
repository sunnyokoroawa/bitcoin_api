using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class ListInvoiceRequest
    {
        public string label { get; set; }
    }

    public class ListInvoiceResponse : ListInvoicesResponseInvoice
    {
         
    }
}
