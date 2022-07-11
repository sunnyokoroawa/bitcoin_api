using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class LoadWalletRequest
    {
        public string Name { get; set; }
    }

    public class LoadWalletResponse
    {
        public string Name { get; set; }
        public string Warning { get; set; }
    }
}
