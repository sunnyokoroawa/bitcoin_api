using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GenerateAddressQRCodeRequest
    {
        public string Address { get; set; }
    }

    public class GenerateAddressQRCodeResponse
    {
        public string ImageURL { get; set; }
        public string Address { get; set; }
        public string ImageBas64String { get; set; }
    }

}
