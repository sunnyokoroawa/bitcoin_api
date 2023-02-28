using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class ConnectRequest
    {
        //nodeid + ip + port
        public string Node { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
    } 


    public class ConnectResponse
    {
        public string id { get; set; }
        public string features { get; set; }
        public string direction { get; set; }
        public ConnectNodeResponseAddress address { get; set; }
    }

    public class ConnectNodeResponseAddress
    {
        public string type { get; set; }
        public string address { get; set; }
        public int port { get; set; }
    }

}
