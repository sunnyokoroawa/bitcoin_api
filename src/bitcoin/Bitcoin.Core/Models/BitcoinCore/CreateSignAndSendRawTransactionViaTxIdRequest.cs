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
    }

    public class FromTransaction
    {
        public string txid { get; set; }
        public int vout { get; set; }
        public string addressPrivateKey { get; set; }
    }

    public class CreateSignAndSendRawTransactionViaTxIdResponse
    {

    }
}
