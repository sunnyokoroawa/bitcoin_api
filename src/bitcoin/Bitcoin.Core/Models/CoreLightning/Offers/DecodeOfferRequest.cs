using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class DecodeOfferRequest
    {
        public string bolt12 { get; set; }
    } 

    public class DecodeOfferResponse
    {
        public DecodeOfferResponse()
        {
            chains = new List<string>();
        }

        public string type { get; set; }
        public string offer_id { get; set; }
        public List<string> chains { get; set; }
        public string amount_msat { get; set; }
        public string description { get; set; }
        public string node_id { get; set; }
        public string signature { get; set; }
        public bool valid { get; set; }
    }

}
