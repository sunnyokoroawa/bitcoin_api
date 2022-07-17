using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class ImportAddressRequest
    {
        public string address { get; set; }
        public string label { get; set; }
        public bool rescan { get; set; }
        //public bool p2sh { get; set; }
    }

    public class ImportAddressResponse
    {

    }
}
