using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class EstimatesMartfeeRequest
    {
        // (numeric, required) Confirmation target in blocks (1 - 1008)
        public int conf_target { get; set; }
        //"unset"
        //"economical"
        //"conservative"
        public string estimate_mode { get; set; }
    }

    public class EstimatesMartfeeResponse
    {
        public decimal feerate { get; set; }
        public int blocks { get; set; }
    }
}
