using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class ListReceivedByAddressRequest : RequestBTC
    {
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class ListReceivedByAddressResponse
    {
        public ListReceivedByAddressResponse()
        {
            txids = new List<string>();
        }

        public string address { get; set; }
        public double amount { get; set; }
        public int confirmations { get; set; }
        public string label { get; set; }
        public List<string> txids { get; set; }
    }


}
