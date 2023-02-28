using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class CreateSignAndSendRawTransactionViaTxIdRequest
    {
        public CreateSignAndSendRawTransactionViaTxIdRequest()
        {
            FromTransactions = new List<FromTransaction>();
        }

        public List<FromTransaction> FromTransactions { get; set; }

        public ReceiveAddressRequest ToAddress { get; set; } 

        /// <summary>
        /// this is in BTC
        /// </summary>
        public decimal? Fees{ get; set; } 
        public string FeeType{ get; set; } //indicates who is to pay the fees

        public string ChangeAddress { get; set; }  

    }

    public class FromTransaction
    {
        public string txid { get; set; }
        public int vout { get; set; }
        public string addressPrivateKey { get; set; }
    }
     

    public class CreateSignAndSendRawTransactionViaTxIdResponse
    {
        public string  TxId { get; set; }
        public int TxIdVOut { get; set; }
    }
}
