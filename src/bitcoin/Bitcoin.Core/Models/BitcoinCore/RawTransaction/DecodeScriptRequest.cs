using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class DecodeScriptRequest
    {
        public string ReceemScript { get; set; }
    }


    public class DecodeScriptResponse
    {
        public string asm { get; set; }
        public string type { get; set; }
        public string p2sh { get; set; }
        public DecodeScriptResponseSegwit segwit { get; set; }
    }

    public class DecodeScriptResponseSegwit
    {
        public string asm { get; set; }
        public string hex { get; set; }
        public string address { get; set; }
        public string type { get; set; }
        public string p2shsegwit { get; set; }
    }

}
