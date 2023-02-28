using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitcoin.API.Services.L1
{ 
    public class GetAddressFromXPUBKeyRequest
    {
        public string XPubKey { get; set; }
        public uint Index { get; set; }
    } 
}
