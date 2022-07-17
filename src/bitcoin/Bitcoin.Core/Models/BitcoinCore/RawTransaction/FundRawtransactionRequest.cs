using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class FundRawtransactionRequest
    {
        public string HexString { get; set; }
        public FundRawtransactionOptionRequest options { get; set; } 
    }
     
    public class FundRawtransactionOptionRequest
    {
        public FundRawtransactionOptionRequest()
        {
            subtractFeeFromOutputs = new List<int>();
        }

        public string changeAddress { get; set; }
        public int changePosition { get; set; }
        public string change_type { get; set; }
        public bool includeWatching { get; set; }
        public bool lockUnspents { get; set; }
        public decimal feeRate { get; set; }
        public List<int> subtractFeeFromOutputs { get; set; }
        public bool replaceable { get; set; }
        public decimal conf_target { get; set; }

        //"UNSET"
        // "ECONOMICAL"
        // "CONSERVATIVE
        public string estimate_mode { get; set; }
    }


    public class FundRawtransactionResponse
    {
        public string hex { get; set; }
        public decimal fee { get; set; }
        public decimal changepos { get; set; }
    }

}
