using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class DumpPrivKeyRequest
    {
        public string Address { get; set; }
    }

    public class DumpPrivKeyResponse
    {
        public string PrivKey { get; set; }
    }
}
