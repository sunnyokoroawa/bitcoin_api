using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class DecodePayRequest
    {
        public string Bolt11 { get; set; }
        public string description { get; set; }
    }

    public class DecodePayResponse
    {
        public string Bolt11 { get; set; }
        public string description { get; set; }
    }
}
