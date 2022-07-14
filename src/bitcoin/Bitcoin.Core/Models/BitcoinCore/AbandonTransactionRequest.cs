using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class AbandonTransactionRequest
    {
        public string TxId { get; set; }
    }

    public class AbandonTransactionResponse
    {
        public string Id { get; set; }
        public BitcoinError Error { get; set; }
    }
}
