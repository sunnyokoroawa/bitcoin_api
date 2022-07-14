using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class ListWalletDirRequest
    {
    }


    public class ListWalletDirResponse
    {
        public ListWalletDirResponse()
        {
            wallets = new List<Wallet>();
        }

        public List<Wallet> wallets { get; set; }
    }

    public class Wallet
    {
        public string name { get; set; }
    }

}
