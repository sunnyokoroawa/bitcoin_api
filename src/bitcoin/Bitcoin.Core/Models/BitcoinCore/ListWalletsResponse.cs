using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class ListWalletsResponse
    {
        public ListWalletsResponse()
        {
            Result = new List<string>();
        }

        public List<string> Result { get; set; }
        public BitcoinError Error { get; set; }
        public int Id { get; set; }
    }


    //public class ListWalletsResponse
    //{
    //    public ListWalletsResponse()
    //    {
    //        Result = new List<string>();
    //    }
    //    public List<string> Result { get; set; }
    //}
     

}
