using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    //createpsbt [{"txid":"hex","vout":n,"sequence":n},...] [{"address":amount},{"data":"hex"},...] ( locktime replaceable )
    public class CreatePsbtRequest
    {
        public CreatePsbtRequest()
        {
            Inputs = new List<CreatePsbtUTXO>();
            Outputs = new List<CreatePsbtUTXOTo>(); 
            Locktime = 101;
            Replaceable = true;
        }

        public List<CreatePsbtUTXO> Inputs { get; set; } //UTXOs
        public List<CreatePsbtUTXOTo> Outputs { get; set; } //Receive addresses
        public int Locktime { get; set; }
        public bool Replaceable { get; set; } //Receive addresses 
    }

    public class CreatePsbtUTXO : CreateRawTransactionInputListRequest
    {

    }

    public class CreatePsbtUTXOTo : ReceiveAddressRequest
    {

    }

    public class CreatePsbtResponse 
    {
        public string Psbt { get; set; }
    }

}
