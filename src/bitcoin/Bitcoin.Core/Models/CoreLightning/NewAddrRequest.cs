using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class NewAddrRequest
    {
        public string addresstype { get; set; }
    }

    public class NewAddrResponse
    {
        public string addresstype { get; set; }
    }
}
