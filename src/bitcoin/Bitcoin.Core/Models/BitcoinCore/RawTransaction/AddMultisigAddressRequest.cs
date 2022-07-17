using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class AddMultisigAddressRequest
    {
        public AddMultisigAddressRequest()
        {
            PublicKeysOrAddresses = new List<string>();
        }

        public int Signatories { get; set; }
        public List<string> PublicKeysOrAddresses { get; set; }
        public string label { get; set; }
        public string address_type { get; set; }
    }

    public class AddMultisigAddressResponse
    {
        public string address { get; set; }
        public string redeemScript { get; set; }
        public string descriptor { get; set; }
    }
}
