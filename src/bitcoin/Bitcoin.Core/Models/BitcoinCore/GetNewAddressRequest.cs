using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetNewAddressRequest : RequestBTC
    {
        public string address_type { get; set; }
        public string label { get; set; }
    }

    
    
}
