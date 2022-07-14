using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetRawChangeAddressRequest
    {
        /// <remarks>
        /// “legacy”, “p2sh-segwit”, and “bech32” and all case sensitive
        /// </remarks>
        public string address_type { get; set; }
    }

    public class GetRawChangeAddressResponse
    {

    }


}
