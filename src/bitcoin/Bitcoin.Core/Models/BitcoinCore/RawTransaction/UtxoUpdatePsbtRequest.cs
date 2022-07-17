using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class UtxoUpdatePsbtRequest
    {
        public string Psbt { get; set; }
    }

    public class UtxoUpdatePsbtResponse
    {
        public string Psbt { get; set; }
    }
}
