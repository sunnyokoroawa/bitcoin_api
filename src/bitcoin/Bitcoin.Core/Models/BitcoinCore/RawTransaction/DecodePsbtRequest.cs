using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class DecodePsbtRequest
    {
        public string Hex { get; set; }
    }

    public class DecodePsbtResponse
    {
        public DecodePsbtResponse()
        {
            inputs = new List<DecodePsbtResponseInput>();
            outputs = new List<DecodePsbtResponseOutput>();
        }

        public DecodePsbtResponseTx tx { get; set; }
        public Unknown unknown { get; set; }
        public List<DecodePsbtResponseInput> inputs { get; set; }
        public List<DecodePsbtResponseOutput> outputs { get; set; }
        public double fee { get; set; }
    }

    public class DecodePsbtResponseBip32Deriv
    {
        public string pubkey { get; set; }
        public string master_fingerprint { get; set; }
        public string path { get; set; }
    }

    public class DecodePsbtResponseInput
    {
        public DecodePsbtResponseInput()
        {
            bip32_derivs = new List<DecodePsbtResponseBip32Deriv>();
        }
        public DecodePsbtResponseWitnessUtxo witness_utxo { get; set; }
        public DecodePsbtResponseNonWitnessUtxo non_witness_utxo { get; set; }
        public List<DecodePsbtResponseBip32Deriv> bip32_derivs { get; set; }
    }

    public class DecodePsbtResponseNonWitnessUtxo
    {
        public DecodePsbtResponseNonWitnessUtxo()
        {
            vin = new List<DecodePsbtResponseVin>();
            vout = new List<DecodePsbtResponseVout>();
        }

        public string txid { get; set; }
        public string hash { get; set; }
        public int version { get; set; }
        public int size { get; set; }
        public int vsize { get; set; }
        public int weight { get; set; }
        public int locktime { get; set; }
        public List<DecodePsbtResponseVin> vin { get; set; }
        public List<DecodePsbtResponseVout> vout { get; set; }
    }

    public class DecodePsbtResponseOutput
    {
        public DecodePsbtResponseOutput()
        {
            bip32_derivs = new List<DecodePsbtResponseBip32Deriv>();
        }

        public List<DecodePsbtResponseBip32Deriv> bip32_derivs { get; set; }
    }
     
    public class DecodePsbtResponseScriptPubKey
    {
        public string asm { get; set; }
        public string hex { get; set; }
        public string address { get; set; }
        public string type { get; set; }
    }

    public class DecodePsbtResponseScriptSig
    {
        public string asm { get; set; }
        public string hex { get; set; }
    }

    public class DecodePsbtResponseTx
    {
        public DecodePsbtResponseTx()
        {
            vin = new List<DecodePsbtResponseVin>();
            vout = new List<DecodePsbtResponseVout>();
        }

        public string txid { get; set; }
        public string hash { get; set; }
        public int version { get; set; }
        public int size { get; set; }
        public int vsize { get; set; }
        public int weight { get; set; }
        public int locktime { get; set; }
        public List<DecodePsbtResponseVin> vin { get; set; }
        public List<DecodePsbtResponseVout> vout { get; set; }
    }

    public class Unknown
    {
        //public Dictionary<string, string> Unknowjn  { get; set; }
    }

    public class DecodePsbtResponseVin
    {
        public string txid { get; set; }
        public int vout { get; set; }
        public DecodePsbtResponseScriptSig scriptSig { get; set; }
        public long sequence { get; set; }
    }

    public class DecodePsbtResponseVout
    {
        public double value { get; set; }
        public int n { get; set; }
        public DecodePsbtResponseScriptPubKey scriptPubKey { get; set; }
    }

    public class DecodePsbtResponseWitnessUtxo
    {
        public double amount { get; set; }
        public DecodePsbtResponseScriptPubKey scriptPubKey { get; set; }
    }

}
