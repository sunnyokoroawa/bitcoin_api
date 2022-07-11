using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetAssetPrizeRequest
    {
        public string Code { get; set; }
    }
     
    public class GetAssetPrizeResponse
    {
        public GetAssetPrizeCoin coin { get; set; }
    }

    public class GetAssetPrizeCoin
    {
        public string id { get; set; }
        public string icon { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public int rank { get; set; }
        public float price { get; set; }
        public int priceBtc { get; set; }
        public float volume { get; set; }
        public float marketCap { get; set; }
        public int availableSupply { get; set; }
        public int totalSupply { get; set; }
        public float priceChange1h { get; set; }
        public float priceChange1d { get; set; }
        public float priceChange1w { get; set; }
        public string websiteUrl { get; set; }
        public string twitterUrl { get; set; }
        public string[] exp { get; set; }
    }

}
