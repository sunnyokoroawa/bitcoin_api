using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    

    public class GetBlockchainInfoResponse
    {
        public string chain { get; set; }
        public int blocks { get; set; }
        public int headers { get; set; }
        public string bestblockhash { get; set; }
        public float difficulty { get; set; }
        public int mediantime { get; set; }
        public float verificationprogress { get; set; }
        public bool initialblockdownload { get; set; }
        public string chainwork { get; set; }
        public long size_on_disk { get; set; }
        public bool pruned { get; set; }
        public Softforks softforks { get; set; }
        public string warnings { get; set; }
    }

    public class Softforks
    {
        public Bip34 bip34 { get; set; }
        public Bip66 bip66 { get; set; }
        public Bip65 bip65 { get; set; }
        public Csv csv { get; set; }
        public Segwit segwit { get; set; }
        public Taproot taproot { get; set; }
    }

    public class Bip34
    {
        public string type { get; set; }
        public bool active { get; set; }
        public int height { get; set; }
    }

    public class Bip66
    {
        public string type { get; set; }
        public bool active { get; set; }
        public int height { get; set; }
    }

    public class Bip65
    {
        public string type { get; set; }
        public bool active { get; set; }
        public int height { get; set; }
    }

    public class Csv
    {
        public string type { get; set; }
        public bool active { get; set; }
        public int height { get; set; }
    }

    public class Segwit
    {
        public string type { get; set; }
        public bool active { get; set; }
        public int height { get; set; }
    }

    public class Taproot
    {
        public string type { get; set; }
        public Bip9 bip9 { get; set; }
        public int height { get; set; }
        public bool active { get; set; }
    }

    public class Bip9
    {
        public string status { get; set; }
        public int start_time { get; set; }
        public int timeout { get; set; }
        public int since { get; set; }
        public int min_activation_height { get; set; }
    }

}
