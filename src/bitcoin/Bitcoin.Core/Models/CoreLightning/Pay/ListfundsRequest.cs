using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class ListfundsRequest
    {
    }


    public class ListfundsResponse
    {
        public ListfundsResponse()
        {
            outputs = new List<ListfundsResponseOutput>();
            channels = new List<ListfundsResponseChannel>();
        }

        public List<ListfundsResponseOutput> outputs { get; set; }
        public List<ListfundsResponseChannel> channels { get; set; }
    }

    public class ListfundsResponseOutput
    {
        public string txid { get; set; }
        public int output { get; set; }
        public int value { get; set; }
        public string amount_msat { get; set; }
        public string scriptpubkey { get; set; }
        public string address { get; set; }
        public string status { get; set; }
        public int blockheight { get; set; }
        public bool reserved { get; set; }
    }

     
    public class ListfundsResponseChannel
    {
        public string peer_id { get; set; }
        public bool connected { get; set; }
        public string state { get; set; }
        public int channel_sat { get; set; }
        public string our_amount_msat { get; set; }
        public int channel_total_sat { get; set; }
        public string amount_msat { get; set; }
        public string funding_txid { get; set; }
        public int funding_output { get; set; }
    }
 
}
