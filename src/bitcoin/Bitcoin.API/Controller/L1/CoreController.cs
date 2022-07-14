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
    [Route("api/v22/[controller]")]
    [ApiController]
    public class BitcoinController : ControllerBase
    {
        private readonly IBitcoinCoreClient client;

        public BitcoinController(IBitcoinCoreClient client)
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
        [Route("validateAddress")]
        public async Task<IActionResult> ValidateAddress(ValidateAddressRequest model)
        {
            Log.Information($"ValidateAddress request {JsonConvert.SerializeObject(model)}");
            var response = await client.ValidateAddressAsync(model);
            Log.Information($"ValidateAddress response {JsonConvert.SerializeObject(response)}");
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
         
        /// <summary>
        /// relies on a 3rd party to get txns
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getTransaction3rdParty")]
        public async Task<IActionResult> GetTransaction3rdParty(GetTransaction3rdPartyRequest model)
        {
            var response = await client.GetTransaction3rdPartyAsync(model);
            Log.Information($"GetTransaction3rdParty response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

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
        [Route("getRawJSONTransaction")]
        public async Task<IActionResult> GetRawJSONTransaction(GetRawTransactionAsJSONRequest model)
        {
            var response = await client.GetRawTransactionAsJSONAsync(model);
            Log.Information($"getRawJSONTransaction response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("createRawTransaction")]
        public async Task<IActionResult> CreateRawTransactionAsync(CreateRawTransactionRequest model)
        {
            var response = await client.CreateRawTransactionAsync(model);
            Log.Information($"CreateRawTransaction response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }
         
        [HttpPost]
        [Route("decodeRawTransaction")]
        public async Task<IActionResult> DecodeRawTransactionAsync(DecodeRawTransactionRequest model)
        {
            var response = await client.DecodeRawTransactionAsync(model);
            Log.Information($"DecodeRawTransaction response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }
         
       
        [HttpPost]
        [Route("SendRawTransaction")]
        public async Task<IActionResult> SendRawTransaction(SendRawTransactionRequest model)
        {
            var response = await client.SendRawTransactionAsync(model);
            Log.Information($"SendRawTransaction response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("postRawTransaction")]
        public async Task<IActionResult> CreateSignAndSendRawTransact(CreateSignAndSendRawTransactionViaTxIdRequest model)
        {
            var response = await client.CreateSignAndSendRawTransactAsync(model);
            Log.Information($"CreateSignAndSendRawTransact response {JsonConvert.SerializeObject(response)}");
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

        [HttpPost]
        [Route("listWallets")]
        public async Task<IActionResult> ListWallets()
        {
            var response = await client.ListWalletsAsync();
            Log.Information($"ListWallets response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

       
        [HttpPost]
        [Route("generateQRCode")]
        public async Task<IActionResult> GenerateQRCode(GenerateQRCodeRequest model)
        {
            var response = await client.GenerateQRCodeAsync(model);
            Log.Information($"GenerateQRCode response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }
    }
}