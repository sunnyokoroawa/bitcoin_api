using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetAddressInfo3rdPartyRequest : GetReceivedByAddressRequest
    { 
    }

    public class GetAddressInfo3rdPartyResponse
    {
        public string address { get; set; }
        public int total_received { get; set; }
        public int total_sent { get; set; }
        public int balance { get; set; }
        public int unconfirmed_balance { get; set; }
        public int final_balance { get; set; }
        public int n_tx { get; set; }
        public int unconfirmed_n_tx { get; set; }
        public int final_n_tx { get; set; }
        public string unit { get; set; }
    }

}
