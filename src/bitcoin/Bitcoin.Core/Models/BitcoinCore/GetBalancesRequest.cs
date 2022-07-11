using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetBalancesRequest : RequestBTC
    {
    }

    public class GetBalancesResponseData
    {
        public double trusted { get; set; }
        public double untrusted_pending { get; set; }
        public double immature { get; set; }
    }

    public class GetBalancesResponse
    {
        public GetBalancesResponseData mine { get; set; }
    }

}
