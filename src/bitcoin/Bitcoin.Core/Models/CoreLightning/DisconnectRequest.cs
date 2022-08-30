using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class DisconnectRequest
    {
        public string NodeId { get; set; }
        public bool? Force { get; set; }
    }


    public class DisconnectResponse
    {
        //public DisconnectResponse()
        //{
        //    peers = new List<DisconnectResponsePeer>();
        //}

        //public List<DisconnectResponsePeer> peers { get; set; }
    }

    public class DisconnectResponsePeer
    {
        public DisconnectResponsePeer()
        {
            netaddr = new List<string>();
        }

        public string id { get; set; }
        public bool connected { get; set; }
        public List<string> netaddr { get; set; }
        public string features { get; set; }
        public ListpeersResponsePeerChannel channels { get; set; }
    }

    public class DisconnectResponsePeerChannel : ListpeersResponsePeerChannel
    {

    }


}
