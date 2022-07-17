using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
   public class FinalizePsbtRequest
    {
        public string Psbt { get; set; }
    }

    public class FinalizePsbtResponse
    {
        public string Psbt { get; set; }
        public string Hex { get; set; }
        public string Complete { get; set; }
    }
}
