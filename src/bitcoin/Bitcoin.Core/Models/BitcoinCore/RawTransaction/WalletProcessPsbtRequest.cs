using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class WalletProcessPsbtRequest
    {
        public string Psbt { get; set; }
    }
     

    public class WalletProcessPsbtResponse
    {
        public string psbt { get; set; }
        public bool complete { get; set; }
    }

}
