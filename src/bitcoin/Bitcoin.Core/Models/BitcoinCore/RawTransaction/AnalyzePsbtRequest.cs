using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class AnalyzePsbtRequest
    {
        public string Psbt { get; set; }
    }


    public class AnalyzePsbtResponse
    {
        public AnalyzePsbtResponse()
        {
            inputs = new List<AnalyzePsbtInput>();
        }

        public List<AnalyzePsbtInput> inputs { get; set; }
        public int estimated_vsize { get; set; }
        public float estimated_feerate { get; set; }
        public float fee { get; set; }
        public string next { get; set; }
    }

    public class AnalyzePsbtInput
    {
        public bool has_utxo { get; set; }
        public bool is_final { get; set; }
        public string next { get; set; }
        public AnalyzePsbtMissing missing { get; set; }
    }

    public class AnalyzePsbtMissing
    {
        public AnalyzePsbtMissing()
        {
            signatures = new List<string>();
        }

        public List<string> signatures { get; set; }
    }

}
