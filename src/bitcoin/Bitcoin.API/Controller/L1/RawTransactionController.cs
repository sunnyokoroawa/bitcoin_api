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
        [Route("createSignSendRawTransaction")]
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
            Log.Information($"getRawJSONTransaction request {JsonConvert.SerializeObject(model)}");
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

        /// <remarks>
        /// Response: it tells you the stage of the multisig txn
        /// { 
        ///  "inputs": [
        ///    {
        ///      "has_utxo": true,
        ///      "is_final": false,
        ///      "next": "signer",
        ///      "missing": {
        ///        "signatures": [
        ///          "46651ff1ba122a0b5a5271b4d41e6486607dd0e1",
        ///          "9f5212ef00d43bd69bbea239a54b9dece1f54036",
        ///          "581025b59cf5739b1b34dfe80edd1a49aa8ae62c"
        ///        ]
        ///    }
        ///}
        ///  ],
        ///  "estimated_vsize": 177,
        ///  "estimated_feerate": 0.00001000,
        ///  "fee": 0.00000177,
        ///  "next": "signer"
        ///}
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("analyzePsbt")]
        public async Task<IActionResult> AnalyzePsbt(AnalyzePsbtRequest model)
        {
            Log.Information($"AnalyzePsbt request {JsonConvert.SerializeObject(model)}");
            var response = await client.AnalyzePsbtAsync(model);
            Log.Information($"AnalyzePsbt response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        /// <summary>
        /// Response: The redeemScript is called when you createmultisig txn
        /// {
        ///  "asm": "2 02e494d8511bd695482c1dd0ac306c976168473df24972fb9b988105bb9bf7d8cd 03fa068d09dbd8ce9f996d965b6251ed56b23314db3ce6d7da7cc4ab4adc843454 02b4c3ab0d87ce53dd6107312100b0a4ec9ae73f40aa9787d0256a099d7b68aa24 3 OP_CHECKMULTISIG",
        ///  "type": "multisig",
        ///  "p2sh": "2NBoSMTcXT9Z4KS9aXPDSrk6gxDiKRigQCY",
        ///  "segwit": {
        ///    "asm": "0 a7e3af14fdfda450488a4233c8389fdc91670985fa2f701f73f193b19160efc2",
        ///    "hex": "0020a7e3af14fdfda450488a4233c8389fdc91670985fa2f701f73f193b19160efc2",
        ///    "address": "tb1q5l36798alkj9qjy2ggeuswylmjgkwzv9lghhq8mn7xfmrytqalpqrjc582",
        ///    "type": "witness_v0_scripthash",
        ///    "p2sh-segwit": "2N5NSZb72e3Htjj2DP2GnoEv7kfZ2rSiE8M"
        ///  }
        ///}
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("decodeScript")]
        public async Task<IActionResult> DecodeScript(DecodeScriptRequest model)
        {
            Log.Information($"DecodeScript request {JsonConvert.SerializeObject(model)}");
            var response = await client.DecodeScriptAsync(model);
            Log.Information($"DecodeScript response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("getDescriptorinfo")]
        public async Task<IActionResult> GetDescriptorinfo(GetDescriptorinfoRequest model)
        {
            Log.Information($"getDescriptorinfo request {JsonConvert.SerializeObject(model)}");
            var response = await client.GetDescriptorinfoAsync(model);
            Log.Information($"getDescriptorinfo response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("generateBlock")]
        public async Task<IActionResult> GenerateBlock(GenerateBlockRequest model)
        {
            Log.Information($"GenerateBlock request {JsonConvert.SerializeObject(model)}");
            var response = await client.GenerateBlockAsync(model);
            Log.Information($"GenerateBlock response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }
    }
}
