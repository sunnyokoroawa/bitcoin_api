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

    public class ResponseCLN<T>
    {
        public string Id { get; set; } 
        public T Result { get; set; }
    } 
}
