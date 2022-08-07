using Bitcoin.Core.Models; 
using Bitcoin.Core.Models.CoreLightning;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bitcoin.Core.Interfaces
{
    public interface ICoreLightningClient
    {
        Task<ResponseCLN<GetInfoResponse>> GetInfoAsync(GetInfoRequest model);
        Task<ResponseCLN<NewAddrResponse>> NewAddrAsync(NewAddrRequest model);

    }
}
