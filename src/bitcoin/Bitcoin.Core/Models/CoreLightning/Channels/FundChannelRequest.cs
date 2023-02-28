using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class FundChannelRequest
    {
        public string PeerId { get; set; }
        public int AmountInSATs { get; set; }
        public int FeeRateInSATs { get; set; }
    }



    public class FundChannelResponse
    {
        public string tx { get; set; }
        public string txid { get; set; }
        public string channel_id { get; set; }
        public int outnum { get; set; }
    }

     
}
