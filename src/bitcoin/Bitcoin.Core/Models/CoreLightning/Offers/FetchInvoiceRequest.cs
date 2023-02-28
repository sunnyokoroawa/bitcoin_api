using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class FetchInvoiceRequest
    {
        public string bolt12 { get; set; }
    }


    public class FetchInvoiceResponse
    {
        public string invoice { get; set; }
        public Changes changes { get; set; }
    }

    public class Changes
    {
    }

}
