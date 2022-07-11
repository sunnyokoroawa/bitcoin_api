using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class CreateRawTransactionRequest
    {
        public CreateRawTransactionRequest()
        {
            SendTransactions = new List<CreateRawTransactionInputListRequest>();
            ReceiveAddresses = new List<ReceiveAddressRequest>();
        }

        public List<CreateRawTransactionInputListRequest> SendTransactions  { get; set; }

        /// <summary>
        /// Addresses and the amount of Bitcoin to send
        /// </summary>
        //public List<Dictionary<string, decimal>> Outputs { get; set; }

        public List<ReceiveAddressRequest> ReceiveAddresses { get; set; } 
        public decimal? Fee { get; set; }
    }

    public class ReceiveAddressRequest
    { 
        public  string  Address{ get; set; }
        public  decimal Amount { get; set; }
    }


    //'[{"txid": "0c8a5c69be9689ee5bf70e45d3ae2a1de03ba985eeb3a0e11a55ac69ff99c751", "vout": 0}]' '[{"tb1qwmf682s0qgwt409ts64392p5zh0ljcperhluwl":0.001}]'
    public class CreateRawTransactionInputRequest
    {
        public CreateRawTransactionInputRequest()
        {
            Inputs = new List<CreateRawTransactionInputListRequest>();
        }

        public List<CreateRawTransactionInputListRequest> Inputs { get; set; }
    }

    public class CreateRawTransactionInputListRequest
    {
        public string txid { get; set; }
        public int vout { get; set; }
    }
     
    public class CreateRawTransactionOutputRequest
    {
        public CreateRawTransactionOutputRequest()
        {
            Outputs = new List<Dictionary<string, decimal>>();
        }

        public List<Dictionary<string, decimal>> Outputs { get; set; }
    }

     


}
