using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class ListChannelsRequest
    {
    }
      
    public class ListChannelsResponse
    {
        public ListChannelsResponse()
        {
            channels = new List<ListChannelsResponseChannel>();
        }

        public List<ListChannelsResponseChannel> channels { get; set; }
    }

    public class ListChannelsResponseChannel
    {
        public string source { get; set; }
        public string destination { get; set; }
        public string short_channel_id { get; set; }
        public bool _public { get; set; }
        public int satoshis { get; set; }
        public string amount_msat { get; set; }
        public int message_flags { get; set; }
        public int channel_flags { get; set; }
        public bool active { get; set; }
        public int last_update { get; set; }
        public int base_fee_millisatoshi { get; set; }
        public int fee_per_millionth { get; set; }
        public int delay { get; set; }
        public string htlc_minimum_msat { get; set; }
        public string htlc_maximum_msat { get; set; }
        public string features { get; set; }
    }

}
