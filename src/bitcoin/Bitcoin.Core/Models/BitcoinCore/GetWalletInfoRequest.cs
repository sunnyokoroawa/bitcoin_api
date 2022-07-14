using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    
     
    public class GetWalletInfoResponse
    {
        public string walletname { get; set; }
        public int walletversion { get; set; }
        public string format { get; set; }
        public float balance { get; set; }
        public float unconfirmed_balance { get; set; }
        public float immature_balance { get; set; }
        public int txcount { get; set; }
        public int keypoololdest { get; set; }
        public int keypoolsize { get; set; }
        public string hdseedid { get; set; }
        public int keypoolsize_hd_internal { get; set; }
        public float paytxfee { get; set; }
        public bool private_keys_enabled { get; set; }
        public bool avoid_reuse { get; set; }
        public bool scanning { get; set; }
        public bool descriptors { get; set; }
    }

}
