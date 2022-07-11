using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class ValidateAddressRequest
    {
        public string Address { get; set; }
    }
     
    public class ValidateAddressResponse
    {
        public bool isvalid { get; set; }
        public string address { get; set; }
        public string scriptPubKey { get; set; }
        public bool isscript { get; set; }
        public bool iswitness { get; set; }
        public int witness_version { get; set; }
        public string witness_program { get; set; }
    }


}
