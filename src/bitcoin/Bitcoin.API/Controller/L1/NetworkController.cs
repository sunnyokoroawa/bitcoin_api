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
    [Route("api/v22/[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        private readonly IBitcoinCoreClient client;

        public NetworkController(IBitcoinCoreClient client)
        {
            this.client = client;
        }

        [HttpPost]
        [Route("estimatesMartfee")]
        public async Task<IActionResult> EstimatesMartfeeAsync(EstimatesMartfeeRequest model)
        {
            Log.Information($"EstimatesMartfeeAsync request {JsonConvert.SerializeObject(model)}");
            var response = await client.EstimatesMartfeeAsync(model);
            Log.Information($"EstimatesMartfeeAsync response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

    }
}
