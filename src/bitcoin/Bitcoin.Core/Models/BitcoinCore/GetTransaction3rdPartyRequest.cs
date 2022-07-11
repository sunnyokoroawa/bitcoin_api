using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetTransaction3rdPartyRequest
    {
        public string TxId { get; set; }
    }


  


    public class GetTransaction3rdPartyResponse
    {
        public GetTransaction3rdPartyResponse()
        {
            spentCoins = new List<GetTransaction3rdPartyResponseSpentcoin>();
            receivedCoins = new List<GetTransaction3rdPartyResponseReceivedcoin>();
        }

        public string transaction { get; set; }
        public string transactionId { get; set; }
        public bool isCoinbase { get; set; }
        public GetTransaction3rdPartyResponseBlock block { get; set; }
        public List<GetTransaction3rdPartyResponseSpentcoin> spentCoins { get; set; }
        public List<GetTransaction3rdPartyResponseReceivedcoin> receivedCoins { get; set; }
        public DateTime firstSeen { get; set; }
        public int fees { get; set; }
    }

    public class GetTransaction3rdPartyResponseBlock
    {
        public string blockId { get; set; }
        public string blockHeader { get; set; }
        public int height { get; set; }
        public int confirmations { get; set; }
        public DateTime medianTimePast { get; set; }
        public DateTime blockTime { get; set; }
    }

    public class GetTransaction3rdPartyResponseSpentcoin
    {
        public string transactionId { get; set; }
        public int index { get; set; }
        public int value { get; set; }
        public string scriptPubKey { get; set; }
        public object redeemScript { get; set; }
    }

    public class GetTransaction3rdPartyResponseReceivedcoin
    {
        public string transactionId { get; set; }
        public int index { get; set; }
        public int value { get; set; }
        public string scriptPubKey { get; set; }
        public object redeemScript { get; set; }
    }

}
