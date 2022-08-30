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

namespace Bitcoin.API.Controller.L1
{
    [Route("api/L1/v22/[controller]")]
    [ApiController]
    public class ThirdPartyController : ControllerBase
    {
        private readonly IBitcoinCoreClient client;

        public ThirdPartyController(IBitcoinCoreClient client)
        {
            this.client = client;
        }

        [HttpPost]
        [Route("getAddressInfo3rdParty")]
        public async Task<IActionResult> GetAddressInfo3rdParty(GetAddressInfo3rdPartyRequest model)
        {
            Log.Information($"GetAddressInfo3rdParty request {JsonConvert.SerializeObject(model)}");
            var response = await client.GetAddressBalance3rdPartyAsync(model);
            Log.Information($"GetAddressInfo3rdParty response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("getTransaction3rdParty")]
        public async Task<IActionResult> GetTransaction3rdParty(GetTransaction3rdPartyRequest model)
        {
            var response = await client.GetTransaction3rdPartyAsync(model);
            Log.Information($"GetTransaction3rdParty response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("GetAssetPrizes")]
        public async Task<IActionResult> GetAssetPrizes()
        {
            var response = await client.GetAssetPrizesAsync();
            Log.Information($"GetAssetPrizes response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("GetAssetPrize")]
        public async Task<IActionResult> GetAssetPrize(GetAssetPrizeRequest model)
        {
            var response = await client.GetAssetPrizeAsync(model);
            Log.Information($"GetAssetPrize response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("getMompoolTransaction")]
        public async Task<IActionResult> GetMompoolTransaction(GetMempoolTransactionRequest model)
        {
            var response = await client.GetMempoolTransactionAsync(model);
            Log.Information($"getMompoolTransaction response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("generateAddressQRCode")]
        public async Task<IActionResult> GenerateAddressQRCodeCode(GenerateAddressQRCodeRequest model)
        {
            var response = await client.GenerateAddressQRCodeAsync(model);
            Log.Information($"GenerateQRCode response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("getRecommendedFee")]
        public async Task<IActionResult> GetRecommendedFee()
        {
            var response = await client.GetRecommendedFeeAsync();
            Log.Information($"GetRecommendedFee response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }
    }
}
