﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public class InvoiceRequest
    {
        public int mSatoshi { get; set; }
        public string labelReference { get; set; } //like reference, used to query txn status

        public string description { get; set; } //like reference, used to query txn status

        public int? expiryInSeconds { get; set; } //if not specified, expiry is 1 week
    }


    public class InvoiceResponse
    {
        public string payment_hash { get; set; }
        public int expires_at { get; set; }
        public string bolt11 { get; set; }
        public string payment_secret { get; set; }
        public string warning_capacity { get; set; }
    }

}
