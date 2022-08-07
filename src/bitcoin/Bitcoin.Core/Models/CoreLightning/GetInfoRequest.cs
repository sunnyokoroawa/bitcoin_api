using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class GetInfoRequest : RequestCLN
    {
    }


    public class GetInfoResponse
    {
        public GetInfoResponse()
        {
            address = new List<GetInfoResponseAddress>();
            binding = new List<GetInfoResponseBinding>();
        }
        public string id { get; set; }
        public string alias { get; set; }
        public string color { get; set; }
        public int num_peers { get; set; }
        public int num_pending_channels { get; set; }
        public int num_active_channels { get; set; }
        public int num_inactive_channels { get; set; }
        public List<GetInfoResponseAddress> address { get; set; }
        public List<GetInfoResponseBinding> binding { get; set; }
        public string version { get; set; }
        public int blockheight { get; set; }
        public string network { get; set; }
        public int msatoshi_fees_collected { get; set; }
        public string fees_collected_msat { get; set; }
        public string lightningdir { get; set; }
        public GetInfoResponseOur_Features our_features { get; set; }
    }

    public class GetInfoResponseAddress
    {
        public string type { get; set; }
        public int port { get; set; }
    }

    public class GetInfoResponseOur_Features
    {
        public string init { get; set; }
        public string node { get; set; }
        public string channel { get; set; }
        public string invoice { get; set; }
    }

    public class GetInfoResponseBinding
    {
        public string type { get; set; }
        public string address { get; set; }
        public int port { get; set; }
    }

}
