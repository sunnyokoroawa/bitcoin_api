using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class CombinePsbtRequest
    {
        public CombinePsbtRequest()
        {
            Psbts = new List<string>();
        }

        public List<string>  Psbts{ get; set; }
    }

    public class CombinePsbResponse
    { 
        public string Psbt { get; set; }
    }
}
