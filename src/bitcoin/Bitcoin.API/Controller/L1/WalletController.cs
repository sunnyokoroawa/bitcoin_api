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
    [Route("api/core/v22/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IBitcoinCoreClient client;
        public WalletController(IBitcoinCoreClient client)
        {
            this.client = client;
        }

        [HttpPost]
        [Route("abandonTransaction")]
        public async Task<IActionResult> AbandonTransaction(AbandonTransactionRequest model)
        {
            Log.Information($"abandonTransaction request {JsonConvert.SerializeObject(model)}");
            var response = await client.AbandonTransactionAsync(model);
            Log.Information($"abandonTransaction response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("createWallet")]
        public async Task<IActionResult> CreateWallet(CreateWalletRequest model)
        {
            Log.Information($"CreateWallet request {JsonConvert.SerializeObject(model)}");
            var response = await client.CreateWalletAsync(model);
            Log.Information($"CreateWallet response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("dumpPrivKey")]
        public async Task<IActionResult> DumpPrivKey(DumpPrivKeyRequest model)
        {
            Log.Information($"DumpPrivKey request {JsonConvert.SerializeObject(model)}");
            var response = await client.DumpPrivKeyAsync(model);
            Log.Information($"DumpPrivKey response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("dumpWallet")]
        public async Task<IActionResult> DumpWallet(DumpWalletRequest model)
        {
            Log.Information($"DumpWallet request {JsonConvert.SerializeObject(model)}");
            var response = await client.DumpWalletAsync(model);
            Log.Information($"DumpWallet response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("bumpFee")]
        public async Task<IActionResult> DumpWallet(BumpFeeRequest model)
        {
            Log.Information($"BumpFee request {JsonConvert.SerializeObject(model)}");
            var response = await client.BumpFeeAsync(model);
            Log.Information($"BumpFee response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("getNewAddress")]
        public async Task<IActionResult> GetNewAddress(GetNewAddressRequest model)
        {
            Log.Information($"getNewAddress request {JsonConvert.SerializeObject(model)}");
            var response = await client.GetNewAddressAsync(model);
            Log.Information($"getNewAddress response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("getBalance")]
        public async Task<IActionResult> getBalance()
        {
            var response = await client.GetBalanceAsync();
            Log.Information($"getBalance response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("getBalances")]
        public async Task<IActionResult> getBalances()
        {
            var response = await client.GetBalancesAsync();
            Log.Information($"getBalances response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("getAddressInfo")]
        public async Task<IActionResult> GetAddressInfo(GetAddressInfoRequest model)
        {
            Log.Information($"getAddressInfo request {JsonConvert.SerializeObject(model)}");
            var response = await client.GetAddressInfoAsync(model);
            Log.Information($"getAddressInfo response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("listReceivedByAddress")]
        public async Task<IActionResult> ListReceivedByAddress()
        {
            var response = await client.ListReceivedByAddressAsync();
            Log.Information($"ListReceivedByAddress response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("getReceivedByAddress")]
        public async Task<IActionResult> GetReceivedByAddress(GetReceivedByAddressRequest model)
        {
            Log.Information($"GetReceivedByAddress request {JsonConvert.SerializeObject(model)}");
            var response = await client.GetReceivedByAddressAsync(model);
            Log.Information($"GetReceivedByAddress response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        /// <summary>
        /// getTransaction only works for txns treated from your own node. The Bitcoin Core desktop
        /// wallet does not store the whole txns, it only stores txns done on the node (inflows and outflows) in
        /// the system's memory.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getTransaction")]
        public async Task<IActionResult> GetTransaction(GetTransactionRequest model)
        {
            Log.Information($"getTransaction request {JsonConvert.SerializeObject(model)}");
            var response = await client.GetTransactionAsync(model);
            Log.Information($"getTransaction response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("getWalletInfo")]
        public async Task<IActionResult> GetWalletInfo()
        { 
            var response = await client.GetWalletInfoAsync();
            Log.Information($"GetWalletInfo response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("listUnspent")]
        public async Task<IActionResult> ListUnspent(ListUnspentRequest model)
        {
            Log.Information($"Unloadwallet request {JsonConvert.SerializeObject(model)}");
            var response = await client.ListUnspentAsync(model);
            Log.Information($"ListUnspent response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("loadWallet")]
        public async Task<IActionResult> LoadWallet(LoadWalletRequest model)
        {
            Log.Information($"Unloadwallet request {JsonConvert.SerializeObject(model)}");
            var response = await client.LoadWalletAsync(model);
            Log.Information($"LoadWallet response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("signRawTransaction")]
        public async Task<IActionResult> SignRawTransaction(SignRawTransactionWithKeysRequest model)
        {
            Log.Information($"Unloadwallet request {JsonConvert.SerializeObject(model)}");
            var response = await client.SignRawTransactionAsync(model);
            Log.Information($"SignRawTransaction response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("unloadwallet")]
        public async Task<IActionResult> Unloadwallet(UnloadWalletRequest model)
        {
            Log.Information($"Unloadwallet request {JsonConvert.SerializeObject(model)}");
            var response = await client.UnloadwalletAsync(model);
            Log.Information($"Unloadwallet response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("listWalletDir")]
        public async Task<IActionResult> ListWalletDir()
        { 
            var response = await client.ListWalletDirAsync();
            Log.Information($"ListWalletDir response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }
        
        /// <remarks>
        /// address_type: “legacy”, “p2sh-segwit”, and “bech32” and all case sensitive
        /// 
        ///     {
        ///        "address_type": "bech32"
        ///     }
        ///     
        /// </remarks>
        [HttpPost]
        [Route("getRawChangeAddress")] //used for change address ONLY
        public async Task<IActionResult> GetRawChangeAddress(GetRawChangeAddressRequest model)
        {
            var response = await client.GetRawChangeAddressAsync(model);
            Log.Information($"GetRawChangeAddress response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("backupWallet")] //used for change address ONLY
        public async Task<IActionResult> BackupWallet(BackupWalletRequest model)
        {
            var response = await client.BackupWalletAsync(model);
            Log.Information($"backupWallet response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("walletlock")] //used for change address ONLY
        public async Task<IActionResult> Walletlock(WalletlockRequest model)
        {
            var response = await client.WalletlockAsync(model);
            Log.Information($"Walletlock response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("walletPassphrase")] //used for change address ONLY
        public async Task<IActionResult> Walletlock(WalletPassphraseRequest model)
        {
            var response = await client.WalletPassphraseAsync(model);
            Log.Information($"WalletPassphrase response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("signMessage")] //used for change address ONLY
        public async Task<IActionResult> SignMessage(SignMessageRequest model)
        {
            var response = await client.SignMessageAsync(model);
            Log.Information($"SignMessage response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("verifySignedMessage")] //used for change address ONLY
        public async Task<IActionResult> SignMessage(VerifySignedMessageRequest model)
        {
            var response = await client.VerifySignedMessageAsync(model);
            Log.Information($"VerifySignedMessage response {JsonConvert.SerializeObject(response)}");
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

    }
}
