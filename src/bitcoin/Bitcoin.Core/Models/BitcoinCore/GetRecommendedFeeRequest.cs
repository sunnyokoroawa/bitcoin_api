using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetRecommendedFeeRequest
    {
    }


    public class GetRecommendedFeeResponse
    {
        public GetRecommendedFeeResponse()
        {
            unit = "sat/vB";
        }

        public int fastestFee { get; set; }
        public int halfHourFee { get; set; }
        public int hourFee { get; set; }
        public int economyFee { get; set; }
        public int minimumFee { get; set; }
        public string unit { get; set; }
    }

}
