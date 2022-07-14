using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class BumpFeeRequest
    {
        public string TxId { get; set; }
    } 

    public class BumpFeeResponse
    {
        public string TxId { get; set; }
        public decimal origfee { get; set; }
        public decimal fee { get; set; }
    }
}
