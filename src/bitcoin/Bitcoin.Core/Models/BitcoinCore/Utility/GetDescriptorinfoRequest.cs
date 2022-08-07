using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetDescriptorinfoRequest
    {
        public string descriptor { get; set; }
    }
 

    public class GetDescriptorinfoResponse
    {
        public string descriptor { get; set; }
        public string checksum { get; set; }
        public bool isrange { get; set; }
        public bool issolvable { get; set; }
        public bool hasprivatekeys { get; set; }
    }

}
