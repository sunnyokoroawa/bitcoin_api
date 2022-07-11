using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetAssetPrizesRequest
    {
         
    }

  

    public class GetAssetPrizesResponse
    {
        public GetAssetPrizesResponse()
        {
            coins = new List<GetAssetPrizesCoin>();
        }

        public List<GetAssetPrizesCoin>  coins { get; set; }
    }

    public class GetAssetPrizesCoin
    {
        public string id { get; set; }
        public string icon { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public int rank { get; set; }
        public float price { get; set; }
        public float priceBtc { get; set; }
        public float? volume { get; set; }
        public float marketCap { get; set; }
        public float availableSupply { get; set; }
        public float totalSupply { get; set; }
        public float priceChange1h { get; set; }
        public float priceChange1d { get; set; }
        public float priceChange1w { get; set; }
        public string websiteUrl { get; set; }
        public string twitterUrl { get; set; }
        public string[] exp { get; set; }
        public string contractAddress { get; set; }
        public int decimals { get; set; }
        public string redditUrl { get; set; }
    }

}
