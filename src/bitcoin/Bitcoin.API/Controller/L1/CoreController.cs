﻿using Bitcoin.Core.Interfaces;
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
    public class BitcoinController : ControllerBase
    {
        private readonly IBitcoinCoreClient client;

        public BitcoinController(IBitcoinCoreClient client)
        {
            this.client = client;
        }



        [HttpPost]
        [Route("validateAddress")]
        public async Task<IActionResult> ValidateAddress(ValidateAddressRequest model)
        {
            Log.Information($"ValidateAddress request {JsonConvert.SerializeObject(model)}");
            var response = await client.ValidateAddressAsync(model);
            Log.Information($"ValidateAddress response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        

        /// <summary>
        /// relies on a 3rd party to get txns
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        /// <summary>
        /// this is used to get the value of UTXO in a wallet address
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getTxOut")]
        public async Task<IActionResult> GetTxOut(GetTxOutRequest model)
        {
            var response = await client.GetTxOutAsync(model);
            Log.Information($"GetTxOut response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("getTxOutSetInfo")]
        public async Task<IActionResult> GetTxOutSetInfo()
        {
            var response = await client.GetTxOutSetInfoAsync();
            Log.Information($"GetTxOutSetInfo response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        /// <summary>
        /// this gets UTXOs by supplying the address
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("getBlock")]
        public async Task<IActionResult> GetBlock(GetBlockRequest model)
        {
            var response = await client.GetBlockAsync(model);
            Log.Information($"getBlock response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }


        [HttpPost]
        [Route("getBlockchainInfo")]
        public async Task<IActionResult> GetBlockchainInfo()
        {
            var response = await client.GetBlockchainInfoAsync();
            Log.Information($"GetBlockchainInfo response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

    }
}
