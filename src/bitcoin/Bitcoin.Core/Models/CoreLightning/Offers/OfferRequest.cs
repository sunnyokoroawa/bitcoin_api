using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class OfferRequest
    {
        public string amountInSats { get; set; }
        public string description { get; set; }
        public string issuer { get; set; }
        public string label { get; set; }
        public string quantity_min { get; set; }
        public string quantity_max { get; set; }
        public int? absolute_expiry_in_seconds { get; set; }
    }


    public class OfferResponse
    {
        public string offer_id { get; set; }
        public bool active { get; set; }
        public bool single_use { get; set; }
        public string bolt12 { get; set; }
        public string bolt12_unsigned { get; set; }
        public bool used { get; set; }
        public bool created { get; set; }
    }

}
