using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.Custom
{
    public class SendBTCToExtRequest : Request
    {

    }

    public class SendBTCToExtResponse
    {
        public string TxId { get; set; }
    }
}
