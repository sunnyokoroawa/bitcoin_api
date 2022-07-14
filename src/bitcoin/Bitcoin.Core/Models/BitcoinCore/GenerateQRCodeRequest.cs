using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GenerateQRCodeRequest
    {
        public string Address { get; set; }
    }

    public class GenerateQRCodeResponse
    {
        public string ImageURL { get; set; }
        public string Address { get; set; }
        public string ImageBas64String { get; set; }
    }

}
