using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class SignMessageRequest
    {
        public string Address { get; set; }
        public string Message { get; set; }
    }
    
}
