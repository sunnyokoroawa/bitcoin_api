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
    public class RawTransactionController : ControllerBase
    {
        private readonly IBitcoinCoreClient client;

        public RawTransactionController(IBitcoinCoreClient client)
        {
            this.client = client;
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
        [Route("sendRawTransaction")]
        public async Task<IActionResult> SendRawTransaction(SendRawTransactionRequest model)
        {
            var response = await client.SendRawTransactionAsync(model);
            Log.Information($"SendRawTransaction response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("createSignAndSendRawTransaction")]
        public async Task<IActionResult> CreateSignAndSendRawTransact(CreateSignAndSendRawTransactionViaTxIdRequest model)
        {
            Log.Information($"CreateSignAndSendRawTransact request {JsonConvert.SerializeObject(model)}");
            var response = await client.CreateSignAndSendRawTransactionAsync(model);
            Log.Information($"CreateSignAndSendRawTransact response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        /// <remarks>
        /// options is optional.
        /// Used to add inputs to a txn until its big enough to meet the value of all its output.
        /// https://chainquery.com/bitcoin-cli/fundrawtransaction
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("fundRawtransaction")]
        public async Task<IActionResult> FundRawtransaction(FundRawtransactionRequest model)
        {
            Log.Information($"FundRawtransaction request {JsonConvert.SerializeObject(model)}");
            var response = await client.FundRawtransactionAsync(model);
            Log.Information($"FundRawtransaction response {JsonConvert.SerializeObject(response)}");
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
        [Route("walletCreateFundedPSBT")]
        public async Task<IActionResult> WalletCreateFundedPSBT(WalletCreateFundedPsbtRequest model)
        {
            Log.Information($"WalletCreateFundedPsbt request {JsonConvert.SerializeObject(model)}");
            var response = await client.WalletCreateFundedPSBTAsync(model);
            Log.Information($"WalletCreateFundedPsbt response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("decodePSBT")]
        public async Task<IActionResult> DecodePSBT(DecodePsbtRequest model)
        {
            Log.Information($"DecodePSBT request {JsonConvert.SerializeObject(model)}");
            var response = await client.DecodePSBTAsync(model);
            Log.Information($"DecodePSBT response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("createPsbt")]
        public async Task<IActionResult> CreatePsbt(CreatePsbtRequest model)
        {
            Log.Information($"CreatePsbt request {JsonConvert.SerializeObject(model)}");
            var response = await client.CreatePsbtAsync(model);
            Log.Information($"CreatePsbt response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("walletProcessPsbt")]
        public async Task<IActionResult> WalletProcessPsbt(WalletProcessPsbtRequest model)
        {
            Log.Information($"WalletProcessPsbt request {JsonConvert.SerializeObject(model)}");
            var response = await client.WalletProcessPsbtAsync(model);
            Log.Information($"WalletProcessPsbt response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("finalizePsbt")]
        public async Task<IActionResult> FinalizePsbt(FinalizePsbtRequest model)
        {
            Log.Information($"FinalizePsbt request {JsonConvert.SerializeObject(model)}");
            var response = await client.FinalizePsbtAsync(model);
            Log.Information($"FinalizePsbt response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("utxoUpdatePsbt")]
        public async Task<IActionResult> UtxoUpdatePsbt(UtxoUpdatePsbtRequest model)
        {
            Log.Information($"UtxoUpdatePsbt request {JsonConvert.SerializeObject(model)}");
            var response = await client.UtxoUpdatePsbtAsync(model);
            Log.Information($"UtxoUpdatePsbt response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("convertToPsbt")]
        public async Task<IActionResult> ConvertToPsbt(ConvertToPsbtRequest model)
        {
            Log.Information($"ConvertToPsbt request {JsonConvert.SerializeObject(model)}");
            var response = await client.ConvertToPsbtAsync(model);
            Log.Information($"ConvertToPsbt response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("combinePsbt")]
        public async Task<IActionResult> CombinePsbt(CombinePsbtRequest model)
        {
            Log.Information($"CombinePsbt request {JsonConvert.SerializeObject(model)}");
            var response = await client.CombinePsbtAsync(model);
            Log.Information($"CombinePsbt response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("analyzePsbt")]
        public async Task<IActionResult> AnalyzePsbt(AnalyzePsbtRequest model)
        {
            Log.Information($"AnalyzePsbt request {JsonConvert.SerializeObject(model)}");
            var response = await client.AnalyzePsbtAsync(model);
            Log.Information($"AnalyzePsbt response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("decodeScript")]
        public async Task<IActionResult> DecodeScript(DecodeScriptRequest model)
        {
            Log.Information($"DecodeScript request {JsonConvert.SerializeObject(model)}");
            var response = await client.DecodeScriptAsync(model);
            Log.Information($"DecodeScript response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }
    }
}
