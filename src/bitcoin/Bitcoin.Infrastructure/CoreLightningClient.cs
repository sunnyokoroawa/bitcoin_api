using Bitcoin.Core.Interfaces;
using Bitcoin.Core.Models;
using Bitcoin.Core.Models.CoreLightning;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bitcoin.Infrastructure
{
    public class CoreLightningClient : ICoreLightningClient
    {
        public async Task<ResponseCLN<GetInfoResponse>> GetInfoAsync(GetInfoRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseCLN<NewAddrResponse>> NewAddrAsync(NewAddrRequest model)
        {
            throw new NotImplementedException();
        }
         
    }
}
