using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class DeriveAddressesRequest
    {
        public string Descriptor { get; set; }
    }

    public class DeriveAddressesResponse
    {
        public DeriveAddressesResponse()
        {
            Addresses = new List<string>();
        }

        public List<string> Addresses { get; set; }
    }


    

}
