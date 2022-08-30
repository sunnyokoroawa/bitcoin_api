using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models
{
    public class RequestCLN
    {
        public RequestCLN()
        {

        }
    }

    public class ResponseCLN<T> : LNError
    {
        //public string Id { get; set; }
        public T Result { get; set; }
        //public LNError Error { get; set; }
    }

    public class LNError
    {
        public int Code { get; set; } //0 is success
        public string Message { get; set; }
        public string Type { get; set; } 
        public string Name { get; set; }  
    } 
}
