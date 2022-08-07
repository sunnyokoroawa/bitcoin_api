using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
   public class WithdrawRequest
    {
        public string DestinationAddress { get; set; }
        public int AmountInSATs { get; set; }
        public decimal FeeRate { get; set; }
    }
}
