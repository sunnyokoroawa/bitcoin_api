using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    class ListNodesRequest
    {
    }


    public class ListNodesResponse
    {
        public ListNodesResponse()
        {
            nodes = new List<ListNodesResponseNode>();
        }

        public List<ListNodesResponseNode> nodes { get; set; }
    }

    public class ListNodesResponseNode
    {
        public string nodeid { get; set; }
    }

}
