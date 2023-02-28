using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning.Invoices
{ 

    public class ListPaysResponse
    {
        public ListPaysResponse()
        {
            pays = new List<ListPaysResponseData>();
        }

        public List<ListPaysResponseData> pays { get; set; }
    }

    public class ListPaysResponseData
    {
        public string bolt11 { get; set; }
        public string destination { get; set; }
        public string payment_hash { get; set; }
        public string status { get; set; }
        public int created_at { get; set; }
        public string preimage { get; set; }
        public string amount_msat { get; set; }
        public string amount_sent_msat { get; set; }
    }


}
