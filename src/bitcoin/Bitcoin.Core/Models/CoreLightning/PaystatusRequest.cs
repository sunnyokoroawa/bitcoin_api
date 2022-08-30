using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class PaystatusRequest
    {
    }


    public class PaystatusResponse
    {
        public PaystatusResponse()
        {
            pay = new List<PaystatusResponsePay>();
        }

        public List<PaystatusResponsePay> pay { get; set; }
    }

    public class PaystatusResponsePay
    {
        public PaystatusResponsePay()
        {
            attempts = new List<PaystatusResponseAttempt>();
        }

        public string bolt11 { get; set; }
        public string amount_msat { get; set; }
        public string destination { get; set; }
        public List<PaystatusResponseAttempt> attempts { get; set; }
    }

    public class PaystatusResponseAttempt
    {
        public string strategy { get; set; }
        public DateTime start_time { get; set; }
        public int age_in_seconds { get; set; }
        public DateTime end_time { get; set; }
        public string state { get; set; }
        public PaystatusResponseSuccess success { get; set; }
    }

    public class PaystatusResponseSuccess
    {
        public int id { get; set; }
        public string payment_preimage { get; set; }
    }

}
