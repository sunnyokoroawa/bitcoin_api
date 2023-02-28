using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class NewAddrRequest
    {
        public string addresstype { get; set; }
    }
     

    public class NewAddrResponse
    {
        public string bech32 { get; set; }

        [JsonProperty("p2sh-segwit")]
        public string p2shsegwit { get; set; }
    }
}
