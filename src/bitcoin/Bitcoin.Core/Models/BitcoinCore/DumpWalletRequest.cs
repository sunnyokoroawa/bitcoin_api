using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class DumpWalletRequest
    {
        public string FileName { get; set; }
    }

    public class DumpWalletResponse
    {
        public string FileName { get; set; }
    }
}
