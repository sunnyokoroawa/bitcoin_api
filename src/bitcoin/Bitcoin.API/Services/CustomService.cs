using Bitcoin.Core.Models;
using Bitcoin.Core.Models.Custom;
using QRCoder;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bitcoin.API.Services
{
    public class CustomService
    {
        public CustomService()
        {

        }

        public async Task<Response<SendBTCToExtResponse>> SentBTCToAddressAsync(SendBTCToExtRequest request)
        { 
            return await Task.FromResult(new Response<SendBTCToExtResponse>
            {
                Success = true,
                Message = "Request Successful",
                Data = new SendBTCToExtResponse
                {
                    TxId = Guid.NewGuid().ToString()
                }
            });
        } 
    }
}
