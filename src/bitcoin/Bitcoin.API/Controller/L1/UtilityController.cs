using Bitcoin.API.Services.L1;
using Bitcoin.Core.Interfaces;
using Bitcoin.Core.Models.BitcoinCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitcoin.API.Controller
{
    [Route("api/L1/v22/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private readonly IBitcoinCoreClient client;

        public UtilityController(IBitcoinCoreClient client)
        {
            this.client = client;
        }
         
        [HttpPost]
        [Route("createMultisig")]
        public async Task<IActionResult> CreateMultisig(CreateMultisigRequest model)
        {
            Log.Information($"CreateMultisig response {JsonConvert.SerializeObject(model)}");
            var response = await client.CreateMultisigAsync(model);
            Log.Information($"CreateMultisig response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("createMultiSigAddress")]
        public async Task<IActionResult> CreateMultiSig(CreateMultisigAddressRequest model)
        {
            Log.Information($"CreateMultiSigAddress response {JsonConvert.SerializeObject(model)}");
            var response = await client.CreateMultiSigAddressAsync(model);
            Log.Information($"CreateMultiSigAddress response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

       
    }
}
