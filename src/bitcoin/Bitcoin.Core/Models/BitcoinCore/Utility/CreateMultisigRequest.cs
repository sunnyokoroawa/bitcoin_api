using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    //The createmultisig RPC creates a P2SH multi-signature address
    //A multi sig address is gotten from the combination of 2 already existing addresses
    public class CreateMultisigRequest
    {
        public CreateMultisigRequest()
        {
            PublicKeys = new List<string>();
        }

        public int nrequired { get; set; }
        public List<string> PublicKeys { get; set; }
        public string address_type { get; set; }
    }

    public class CreateMultiSigResponse
    {
        public string address { get; set; }
        public string redeemScript { get; set; }
    }
}
