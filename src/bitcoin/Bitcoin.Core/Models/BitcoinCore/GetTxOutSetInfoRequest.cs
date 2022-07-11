using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetTxOutsetInfoRequest
    {
    }


    public class GetTxOutsetInfoResponse
    {
        public int height { get; set; }
        public string bestblock { get; set; }
        public int txouts { get; set; }
        public long bogosize { get; set; }
        public string hash_serialized_2 { get; set; }
        public float total_amount { get; set; }
        public int transactions { get; set; }
        public long disk_size { get; set; }
    }

}
