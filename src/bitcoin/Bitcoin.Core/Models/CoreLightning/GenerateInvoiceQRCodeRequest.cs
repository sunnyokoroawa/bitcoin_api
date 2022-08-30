using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class GenerateInvoiceQRCodeRequest
    {
        public string Invoice { get; set; }
    }

    public class GenerateInvoiceQRCodeResponse
    {
        public string Invoice { get; set; }
        public string ImageURL { get; set; }
        public string ImageBas64String { get; set; }
    }
}
