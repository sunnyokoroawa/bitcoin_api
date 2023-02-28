using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetNewAddressQRCodeRequest : RequestBTC
    {
        public string Address { get; set; }
        public string address_type { get; set; }
        public string label { get; set; }
    }

    public class GetNewAddressQRCodeResponse : GenerateAddressQRCodeResponse
    {
        public string PriKey { get; set; }
    }
}
