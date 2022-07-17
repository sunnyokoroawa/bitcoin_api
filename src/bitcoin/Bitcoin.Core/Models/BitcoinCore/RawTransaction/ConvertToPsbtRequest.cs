using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class ConvertToPsbtRequest
    {
        public string hexstring { get; set; } //hex string from rawtransaction
        public bool? permitsigdata { get; set; }
        public bool? iswitness { get; set; }
        
    }

    public class ConvertToPsbtResponse
    {
        public string psbt { get; set; }  

    }
}
