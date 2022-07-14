using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
   public class CreateWalletRequest
    {
        public string Name  { get; set; }
    }

    public class CreateWalletResponse
    {
        public string Name { get; set; }
        public string Warning { get; set; }
    }
}
