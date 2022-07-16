using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class VerifySignedMessageRequest
    {
        public string Address { get; set; } //must be a legacy address
        public string Signature { get; set; }
        public string Message { get; set; }
    }

  
}
