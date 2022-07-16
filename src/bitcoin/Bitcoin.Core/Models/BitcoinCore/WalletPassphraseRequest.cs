using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
   public  class WalletPassphraseRequest
    {
        public string PassPhrase  { get; set; }
        public int Timeout { get; set; }
    }

    public class WalletPassphraseResponse
    {
         
    }
}
