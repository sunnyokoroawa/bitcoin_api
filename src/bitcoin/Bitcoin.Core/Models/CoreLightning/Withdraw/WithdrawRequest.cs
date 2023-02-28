using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning.Withdraw
{
    public class WithdrawRequest
    {
        public string ReceiveAddress { get; set; }
        public int AmountInSats { get; set; }
    }


    public class WithdrawResponse
    {
        public string tx { get; set; }
        public string txid { get; set; }
        public string psbt { get; set; }
    }

}
