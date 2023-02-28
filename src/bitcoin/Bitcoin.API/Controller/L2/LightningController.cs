using Bitcoin.Core.Interfaces;
using Bitcoin.Core.Models.CoreLightning;
using Bitcoin.Core.Models.CoreLightning.Invoices;
using Bitcoin.Core.Models.CoreLightning.Withdraw;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitcoin.API.Controller.L2
{
    [Route("api/L2/v0.11.2/[controller]")]
    [ApiController]
    public class LightningController : ControllerBase
    {
        private readonly ICoreLightningClient _client;

        public LightningController(ICoreLightningClient client)
        {
            this._client = client;
        }

        [HttpPost]
        [Route("getInfo")]
        public async Task<IActionResult> GetInfo()
        {
            var response = await _client.GetInfoAsync();
            Log.Information($"getInfo response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("newAddr")]
        public async Task<IActionResult> NewAddr(NewAddrRequest model)
        {
            Log.Information($"newAddr request {JsonConvert.SerializeObject(model)}");
            var response = await _client.NewAddrAsync(model);
            Log.Information($"newAddr response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("listFunds")]
        public async Task<IActionResult> listFunds()
        { 
            var response = await _client.ListFundsAsync();
            Log.Information($"listFunds response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        /// <summary>
        /// It is possible to generate an invoice without specifyng any amount. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("invoice")]
        public async Task<IActionResult> Invoice(InvoiceRequest model)
        {
            Log.Information($"Invoice request {JsonConvert.SerializeObject(model)}");
            var response = await _client.InvoiceAsync(model);
            Log.Information($"Invoice response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("listInvoices")]
        public async Task<IActionResult> ListInvoices()
        { 
            var response = await _client.ListInvoicesAsync();
            Log.Information($"ListInvoices response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("payInvoice")]
        public async Task<IActionResult> Pay(PayRequest model)
        {
            Log.Information($"PayInvoice request {JsonConvert.SerializeObject(model)}");
            var response = await _client.PayAsync(model);
            Log.Information($"PayInvoice response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("listInvoice")]
        public async Task<IActionResult> ListInvoices(ListInvoiceRequest model)
        {
            Log.Information($"Invoice request {JsonConvert.SerializeObject(model)}");
            var response = await _client.ListInvoiceAsync(model);
            Log.Information($"ListInvoices response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("getInvoiceStatus")]
        public async Task<IActionResult> GetInvoiceStatus(GetInvoiceStatusRequest model)
        {
            Log.Information($"GetInvoiceStatus request {JsonConvert.SerializeObject(model)}");
            var response = await _client.GetInvoiceStatusAsync(model);
            Log.Information($"GetInvoiceStatus response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("generateInvoiceQRCode")]
        public async Task<IActionResult> GenerateInvoiceQRCode(GenerateInvoiceQRCodeRequest model)
        {
            Log.Information($"GenerateInvoiceQRCode request {JsonConvert.SerializeObject(model)}");
            var response = await _client.GenerateInvoiceQRCodeAsync(model);
            Log.Information($"GenerateInvoiceQRCode response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("connectToNode")]
        public async Task<IActionResult> ConnectToNode(ConnectRequest model)
        {
            Log.Information($"ConnectToNode request {JsonConvert.SerializeObject(model)}");
            var response = await _client.ConnectAsync(model);
            Log.Information($"ConnectToNode response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("disconnect")]
        public async Task<IActionResult> Disconnect(DisconnectRequest model)
        {
            Log.Information($"Disconnect request {JsonConvert.SerializeObject(model)}");
            var response = await _client.DisconnectAsync(model);
            Log.Information($"Disconnect response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }


        [HttpPost]
        [Route("fundChannel")]
        public async Task<IActionResult> FundChannel(FundChannelRequest model)
        {
            Log.Information($"FundChannel request {JsonConvert.SerializeObject(model)}");
            var response = await _client.FundChannelAsync(model);
            Log.Information($"FundChannel response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }


        [HttpPost]
        [Route("listPeers")]
        public async Task<IActionResult> Listpeers()
        {
            var response = await _client.ListpeersAsync();
            Log.Information($"Listpeers response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("listNodes")]
        public async Task<IActionResult> ListNodes()
        {
            var response = await _client.ListNodesAsync();
            Log.Information($"ListNodes response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("listChannels")]
        public async Task<IActionResult> ListChannels()
        {
            var response = await _client.ListChannelsAsync();
            Log.Information($"ListChannels response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("decodeInvoice")]
        public async Task<IActionResult> DecodeInvoice(DecodeInvoiceRequest model)
        {
            Log.Information($"DecodeInvoice request {JsonConvert.SerializeObject(model)}");
            var response = await _client.DecodeInvoiceAsync(model);
            Log.Information($"DecodeInvoice response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("deleteInvoice")]
        public async Task<IActionResult> DelInvoice(DeleteInvoiceRequest model)
        {
            Log.Information($"deleteInvoice request {JsonConvert.SerializeObject(model)}");
            var response = await _client.DelInvoiceAsync(model);
            Log.Information($"deleteInvoice response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("delexpiredinvoice")]
        public async Task<IActionResult> Delexpiredinvoice()
        { 
            var response = await _client.DelexpiredinvoiceAsync();
            Log.Information($"Delexpiredinvoice response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("decodePay")]
        public async Task<IActionResult> DecodePay(DecodePayRequest model)
        {
            Log.Information($"DecodePay request {JsonConvert.SerializeObject(model)}");
            var response = await _client.DecodePayAsync(model);
            Log.Information($"DecodePay response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("paystatus")]
        public async Task<IActionResult> Paystatus()
        {
            var response = await _client.PaystatusAsync();
            Log.Information($"Paystatus response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("offer")]
        public async Task<IActionResult> Offer(OfferRequest model)
        {
            Log.Information($"Offer response {JsonConvert.SerializeObject(model)}");
            var response = await _client.OfferAsync(model);
            Log.Information($"Offer response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("decodeOffer")]
        public async Task<IActionResult> DecodeOffer(DecodeOfferRequest model)
        {
            Log.Information($"DecodeOffer response {JsonConvert.SerializeObject(model)}");
            var response = await _client.DecodeOfferAsync(model);
            Log.Information($"DecodeOffer response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }

        [HttpPost]
        [Route("withdraw")]
        public async Task<IActionResult> Withdraw(WithdrawRequest model)
        {
            Log.Information($"Withdraw response {JsonConvert.SerializeObject(model)}");
            var response = await _client.WithdrawAsync(model);
            Log.Information($"Withdraw response {JsonConvert.SerializeObject(response)}");
            return await Task.FromResult(new JsonResult(response));
        }
    }
}
