using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models
{
    public class ResponseBTC<T>
    {
        public string Id { get; set; }
        public BitcoinError Error { get; set; }
        public T Result { get; set; }
    }

    public class BitcoinError
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class ValidateResponseBTC<T>
    {
        public string Id { get; set; }
        public ValidateBitcoinError Error { get; set; }
        public T Result { get; set; }
    }


    public class ValidateBitcoinError
    {
        public bool IsValid { get; set; }
        public string Error { get; set; }
    }
}
