using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        //public static implicit operator List<T>(Response<List<OxfordVest.API.Models.UserInvestment.UserInvestmentROIChartModel>> v)
        //{
        //    throw new NotImplementedException();
        //}
    }

    
}
