using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class ImportDescriptorsRequest
    {
        public string Desc   { get; set; }
    }

    public class ImportDescriptorsResponse
    {
        public ImportDescriptorsResponse()
        {
            warning = new List<string>();
        }

        public bool success { get; set; }
        public List<string> warning { get; set; }
        public string error { get; set; }
    }
}
