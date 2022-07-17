using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class WalletCreateFundedPsbtRequest
    {
        public WalletCreateFundedPsbtRequest()
        {
            Inputs = new List<WalletCreateFundedPsbtUTXO>();
            Outputs = new List<WalletCreateFundedPsbtTo>();

            Bip32derivs = true;
            Locktime = 101;
        }

        public List<WalletCreateFundedPsbtUTXO> Inputs { get; set; } //UTXOs

        public List<WalletCreateFundedPsbtTo> Outputs { get; set; } //Receive addresses

        public int Locktime { get; set; }
        public WalletCreateFundedPsbtOptions Options { get; set; } //Receive addresses

        public bool Bip32derivs { get; set; }

    }

    public class WalletCreateFundedPsbtOptions //: FundRawtransactionOptionRequest
    { 
        public bool includeWatching { get; set; }
        //public int subtractFeeFromOutputs { get; set; } //the index to take fee from
    }

    public class WalletCreateFundedPsbtUTXO : CreateRawTransactionInputListRequest
    {
        public int Sequence { get; set; }
    }

    public class WalletCreateFundedPsbtTo : ReceiveAddressRequest
    {

    }

    public class WalletCreateFundedPsbtToData
    {
        public string Data { get; set; } //hex
    }
     

    public class WalletCreateFundedPsbtResponse
    {
        public string psbt { get; set; }
        public float fee { get; set; }
        public int changepos { get; set; }
    }

}
