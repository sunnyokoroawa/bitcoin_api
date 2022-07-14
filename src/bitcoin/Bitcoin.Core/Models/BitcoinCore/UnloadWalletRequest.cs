using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class UnloadWalletRequest
    {
        public string Name { get; set; }
    }

    public class UnloadWalletResponse
    {
        public string Warning { get; set; }
    }

    
}
