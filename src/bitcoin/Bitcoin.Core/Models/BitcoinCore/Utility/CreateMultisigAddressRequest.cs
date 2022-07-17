using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class CreateMultisigAddressRequest
    {
        public int SignatoriesToApprove { get; set; }
        public int Signatories { get; set; }
        public string address_type { get; set; }
    }

    public class CreateMultisigAddressResponse
    {
        public CreateMultisigAddressResponse()
        {
            NAddressesInfo = new List<AddressInfo>();
        }

        public List<AddressInfo> NAddressesInfo { get; set; }
        public MultiSigAddress MultisigAddress { get; set; }
    }

    public class AddressInfo //: GetAddressInfoResponse
    {
        public string Address { get; set; }
        public string Pubkey { get; set; }
    }

    public class MultiSigAddress
    {
        public string address { get; set; }
        public string redeemScript { get; set; }
        public string descriptor { get; set; }
    }
}
