using Bitcoin.Core.Interfaces;
using Bitcoin.Core.Models;
using Bitcoin.Core.Models.BitcoinCore;
using Bitcoin.Core.Models.CoreLightning;
using Bitcoin.Core.Models.CoreLightning.Invoices;
using Bitcoin.Core.Models.CoreLightning.Withdraw;
using Bitcoin.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QRCoder;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitcoin.Infrastructure
{
    public class CoreLightningClient : ICoreLightningClient
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UtilityService _utilityService;

        public CoreLightningClient(IConfiguration config,
            IHttpContextAccessor httpContextAccessor, UtilityService utilityService)
        {
            this._config = config;
            _httpContextAccessor = httpContextAccessor;
            this._utilityService = utilityService;
        }
        private RestRequest CreateRestClientRequest()
        {
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("X-Access", $"{_config["Lightning:Sparko:masterKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            return request;
        }

        public async Task<ResponseCLN<GetInfoResponse>> GetInfoAsync()
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.getinfo, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"GetInfoAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"GetInfoAsync Response: {result.Content}");


            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<GetInfoResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"GetInfo Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<GetInfoResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "GetInfo Error",
                        Type = "GetInfo"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<GetInfoResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "GetInfo Error",
                        Type = "GetInfo"
                    });
            }

            var response = JsonConvert.DeserializeObject<GetInfoResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<GetInfoResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<GetInfoResponse>
            {
                Result = response,
            });
        }

        public async Task<ResponseCLN<NewAddrResponse>> NewAddrAsync(NewAddrRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.addresstype };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.newaddr, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"NewAddrAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"NewAddrAsync Response: {result.Content}");


            //check for error object  
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<NewAddrResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"NewAddr Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<NewAddrResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "NewAddr Error",
                        Type = "Invoice"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<NewAddrResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "NewAddr Error",
                        Type = "NewAddr"
                    });
            }

            var response = JsonConvert.DeserializeObject<NewAddrResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<NewAddrResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<NewAddrResponse>
            {
                Result = response,
            });
        }

        public async Task<ResponseCLN<ListfundsResponse>> ListFundsAsync()
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.listfunds, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"ListFundsAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"ListFundsAsync Response: {result.Content}");


            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<ListfundsResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"ListFunds Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<ListfundsResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "ListFunds Error",
                        Type = "ListFunds"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<ListfundsResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "ListFunds Error",
                        Type = "ListFunds"
                    });
            }

            var response = JsonConvert.DeserializeObject<ListfundsResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<ListfundsResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<ListfundsResponse>
            {
                Result = response,
            });
        }

        public async Task<ResponseCLN<ListpeersResponse>> ListpeersAsync()
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.listpeers, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"ListpeersAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"ListpeersAsync Response: {result.Content}");


            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<ListpeersResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"Listpeers Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<ListpeersResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "Listpeers Error",
                        Type = "Listpeers"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<ListpeersResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "Listpeers Error",
                        Type = "Listpeers"
                    });
            }

            var response = JsonConvert.DeserializeObject<ListpeersResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<ListpeersResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<ListpeersResponse>
            {
                Result = response,
            });
        }

        /// <summary>
        /// any applies when no amount is specified.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseCLN<InvoiceResponse>> InvoiceAsync(InvoiceRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { string.IsNullOrEmpty(model.AmountInSATs) ? "any" : model.AmountInSATs,
                !string.IsNullOrEmpty(model.label) ? model.label : Guid.NewGuid().ToString(),
                model.description, model.expiryInSeconds };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.invoice, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"InvoiceAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"InvoiceAsync Response: {result.Content}");

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<InvoiceResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"Invoice Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<InvoiceResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "Invoice Error",
                        Type = "Invoice"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<InvoiceResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "Invoice Error",
                        Type = "Invoice"
                    });
            }

            var response = JsonConvert.DeserializeObject<InvoiceResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<InvoiceResponse>
                {
                    Message = "No response from API"
                });

            //generate invoice image
            var gerenateQRInvoice = await GenerateInvoiceQRCodeAsync(new GenerateInvoiceQRCodeRequest
            {
                Invoice = response.bolt11
            });

            response.imageBas64String = gerenateQRInvoice.Code >= 0 ? gerenateQRInvoice.Result.ImageBas64String : string.Empty;

            return await Task.FromResult(new ResponseCLN<InvoiceResponse>
            {
                Result = response,
            });
        }

        public async Task<ResponseCLN<ListInvoicesResponse>> ListInvoicesAsync()
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.listinvoices, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"ListInvoicesAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"ListInvoicesAsync Response: {result.Content}");


            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<ListInvoicesResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"ListInvoices Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<ListInvoicesResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "ListInvoices Error",
                        Type = "ListInvoices"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<ListInvoicesResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "ListInvoices Error",
                        Type = "Invoice"
                    });
            }

            var response = JsonConvert.DeserializeObject<ListInvoicesResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<ListInvoicesResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<ListInvoicesResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<ListInvoiceResponse>> ListInvoiceAsync(ListInvoiceRequest model)
        {
            var listInvoicesResponse = await ListInvoicesAsync();

            if (listInvoicesResponse.Code != 0)
                return await Task.FromResult(new ResponseCLN<ListInvoiceResponse>
                {
                    Code = listInvoicesResponse.Code,
                    Message = listInvoicesResponse.Message,
                    Name = listInvoicesResponse.Name,
                    Type = listInvoicesResponse.Type
                });

            //was a success
            var actualResult = listInvoicesResponse.Result.invoices.FirstOrDefault(x => x.label == model.label);

            if (actualResult == null)
                return await Task.FromResult(new ResponseCLN<ListInvoiceResponse>
                {
                    Code = -1,
                    Message = $"No record found for invoice with label/reference {model.label}"
                });

            return await Task.FromResult(new ResponseCLN<ListInvoiceResponse>
            {
                Result = new ListInvoiceResponse
                {
                    amount_msat = actualResult.amount_msat,
                    label = actualResult.label,
                    bolt11 = actualResult.bolt11,
                    description = actualResult.description,
                    expires_at = actualResult.expires_at,
                    msatoshi = actualResult.msatoshi,
                    payment_hash = actualResult.payment_hash,
                    status = actualResult.status
                },
                Message = "Request Successful",
                Code = 0
            });
        }

        public async Task<ResponseCLN<GetInvoiceStatusResponse>> GetInvoiceStatusAsync(GetInvoiceStatusRequest model)
        {
            var listInvoicesResponse = await ListInvoicesAsync();

            //if no error
            if (listInvoicesResponse.Code != 0)
                return await Task.FromResult(new ResponseCLN<GetInvoiceStatusResponse>
                {
                    Code = listInvoicesResponse.Code,
                    Message = listInvoicesResponse.Message,
                    Name = listInvoicesResponse.Name,
                    Type = listInvoicesResponse.Type
                });

            if (!string.IsNullOrEmpty(model.Address))
                listInvoicesResponse.Result.invoices = listInvoicesResponse.Result.invoices.Where(x => x.bolt11 == model.Address)
                    .ToList();

            if (!string.IsNullOrEmpty(model.Label))
                listInvoicesResponse.Result.invoices = listInvoicesResponse.Result.invoices.Where(x => x.label == model.Label)
                    .ToList();

            //was a success
            var actualResult = listInvoicesResponse.Result.invoices.FirstOrDefault();

            if (actualResult == null)
                return await Task.FromResult(new ResponseCLN<GetInvoiceStatusResponse>
                {
                    Code = -1,
                    Message = $"No record found for invoice with label/reference {model.Label} Address: {model.Address}"
                });

            return await Task.FromResult(new ResponseCLN<GetInvoiceStatusResponse>
            {
                Result = new GetInvoiceStatusResponse
                {
                    amount_msat = actualResult.amount_msat,
                    label = actualResult.label,
                    bolt11 = actualResult.bolt11,
                    bolt12 = actualResult.bolt12,
                    description = actualResult.description,
                    expires_at = actualResult.expires_at,
                    msatoshi = actualResult.msatoshi,
                    payment_hash = actualResult.payment_hash,
                    status = actualResult.status
                },
                Message = "Request Successful",
                Code = 0
            });
        }

        public async Task<ResponseCLN<GenerateInvoiceQRCodeResponse>> GenerateInvoiceQRCodeAsync(GenerateInvoiceQRCodeRequest model)
        {
            if (string.IsNullOrEmpty(model.Invoice))
                return await Task.FromResult(new ResponseCLN<GenerateInvoiceQRCodeResponse>
                {
                    Message = "Declined - Invoice cannot be empty",
                    Code = -1
                });

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(model.Invoice, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            // logo path
            var logopath = Path.Combine(
                       Directory.GetCurrentDirectory(), $"{"Docs/Invoice"}", "lnlogo.JPG");

            var generateQRCodeResponse = _utilityService.GenerateQRCodeAsync(model.Invoice, logopath);

            if (!generateQRCodeResponse.Success)
                return await Task.FromResult(new ResponseCLN<GenerateInvoiceQRCodeResponse>
                {
                    Message = generateQRCodeResponse.Message
                });

            return await Task.FromResult(new ResponseCLN<GenerateInvoiceQRCodeResponse>
            {
                Message = "Request Succesful",
                Result = new GenerateInvoiceQRCodeResponse
                {
                    ImageURL = string.Empty,
                    Invoice = model.Invoice,
                    ImageBas64String = generateQRCodeResponse.Data// _utilityService.ConvertToBase64String(filePath)
                }
            });
        }

        public async Task<ResponseCLN<ConnectResponse>> ConnectAsync(ConnectRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { $"{model.Node}@{model.IP}:{model.Port}" };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.connect, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"ConnectAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"ConnectAsync Response: {result.Content}");


            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<ConnectResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"Connect Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<ConnectResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "Connect Error",
                        Type = "Connect"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<ConnectResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "Connect Error",
                        Type = "Connect"
                    });
            }

            var response = JsonConvert.DeserializeObject<ConnectResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<ConnectResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<ConnectResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<FundChannelResponse>> FundChannelAsync(FundChannelRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.PeerId, model.AmountInSATs, model.FeeRateInSATs };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.fundchannel, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"FundChannelAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"FundChannelAsync Response: {result.Content}");



            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<FundChannelResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"FundChannel Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<FundChannelResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "FundChannel Error",
                        Type = "FundChannel"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<FundChannelResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "FundChannel Error",
                        Type = "FundChannel"
                    });
            }

            var response = JsonConvert.DeserializeObject<FundChannelResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<FundChannelResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<FundChannelResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<ListNodesResponse>> ListNodesAsync()
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.listnodes, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"ListNodesAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"ListNodesAsync Response: {result.Content}");



            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<ListNodesResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"ListNodes Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<ListNodesResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "ListNodes Error",
                        Type = "ListNodes"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<ListNodesResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "ListNodes Error",
                        Type = "ListNodes"
                    });
            }

            var response = JsonConvert.DeserializeObject<ListNodesResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<ListNodesResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<ListNodesResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<ListChannelsResponse>> ListChannelsAsync()
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.listchannels, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"ListChannelsAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"ListChannelsAsync Response: {result.Content}");



            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<ListChannelsResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"ListChannels Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<ListChannelsResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "ListChannels Error",
                        Type = "ListChannels"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<ListChannelsResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "ListChannels Error",
                        Type = "ListChannels"
                    });
            }

            var response = JsonConvert.DeserializeObject<ListChannelsResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<ListChannelsResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<ListChannelsResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<PayResponse>> PayAsync(PayRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            //object[] @params = { model.bolt11, model.msatoshi, model.label };
            object[] @params = { model.bolt11}; //no need specifying amts as invoice are generated with the sats amount specified

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.pay, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);


            Log.Information($"PayAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"PayAsync Response: {result.Content}");

            //check for error object  
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<PayResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"Pay Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<PayResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "Pay Error",
                        Type = "Pay"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<PayResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "Pay Error",
                        Type = "Pay"
                    });
            }

            var response = JsonConvert.DeserializeObject<PayResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<PayResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<PayResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<DisconnectResponse>> DisconnectAsync(DisconnectRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.NodeId, model.Force };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.disconnect, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"DisconnectAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"DisconnectAsync Response: {result.Content}");

            //check for error object  
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<DisconnectResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"Disconnect Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<DisconnectResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "Disconnect Error",
                        Type = "Disconnect"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<DisconnectResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "Disconnect Error",
                        Type = "Disconnect"
                    });
            }

            var response = JsonConvert.DeserializeObject<DisconnectResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<DisconnectResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<DisconnectResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<DecodeInvoiceResponse>> DecodeInvoiceAsync(DecodeInvoiceRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Invoice };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.decode, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"DecodeInvoiceAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"DecodeInvoiceAsync Response: {result.Content}");


            //check for error object  
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<DecodeInvoiceResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"DecodeInvoice Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<DecodeInvoiceResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "DecodeInvoice Error",
                        Type = "DecodeInvoice"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<DecodeInvoiceResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "DecodeInvoice Error",
                        Type = "DecodeInvoice"
                    });
            }

            var response = JsonConvert.DeserializeObject<DecodeInvoiceResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<DecodeInvoiceResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<DecodeInvoiceResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<DeleteInvoiceResponse>> DelInvoiceAsync(DeleteInvoiceRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.label, model.status, model.desconly };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.delinvoice, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"DelInvoiceAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"DelInvoiceAsync Response: {result.Content}");

            //check for error object  
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<DeleteInvoiceResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"DelInvoice Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<DeleteInvoiceResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "DelInvoice Error",
                        Type = "DelInvoice"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<DeleteInvoiceResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "DelInvoice Error",
                        Type = "DelInvoice"
                    });
            }

            var response = JsonConvert.DeserializeObject<DeleteInvoiceResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<DeleteInvoiceResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<DeleteInvoiceResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<DelexpiredinvoiceResponse>> DelexpiredinvoiceAsync()
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.delexpiredinvoice, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            Log.Information($"DelexpiredinvoiceAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"DelexpiredinvoiceAsync Response: {result.Content}");

            //check for error object  
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<DelexpiredinvoiceResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"Delexpiredinvoice Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<DelexpiredinvoiceResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "Delexpiredinvoice Error",
                        Type = "Delexpiredinvoice"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<DelexpiredinvoiceResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "Delexpiredinvoice Error",
                        Type = "Delexpiredinvoice"
                    });
            }

            var response = JsonConvert.DeserializeObject<DelexpiredinvoiceResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<DelexpiredinvoiceResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<DelexpiredinvoiceResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<DecodePayResponse>> DecodePayAsync(DecodePayRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Bolt11, model.description };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.decodepay, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"DecodePayAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"DecodePayAsync Response: {result.Content}");

            //check for error object  
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<DecodePayResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"Delexpiredinvoice Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<DecodePayResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "DecodePay Error",
                        Type = "DecodePay"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<DecodePayResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "DecodePay Error",
                        Type = "DecodePay"
                    });
            }

            var response = JsonConvert.DeserializeObject<DecodePayResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<DecodePayResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<DecodePayResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<PaystatusResponse>> PaystatusAsync()
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.paystatus, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"PaystatusAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"PaystatusAsync Response: {result.Content}");

            //check for error object  
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<PaystatusResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"Paystatus Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<PaystatusResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "Paystatus Error",
                        Type = "Paystatus"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<PaystatusResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "Paystatus Error",
                        Type = "Paystatus"
                    });
            }

            var response = JsonConvert.DeserializeObject<PaystatusResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<PaystatusResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<PaystatusResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<OfferResponse>> OfferAsync(OfferRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { !string.IsNullOrEmpty(model.amountInSats) ? model.amountInSats : "any",
                model.description, model.issuer, model.label };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.offer, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);


            Log.Information($"OfferAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"OfferAsync Response: {result.Content}");

            //check for error object  
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<OfferResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"Paystatus Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<OfferResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "Offer Error",
                        Type = "Offer"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<OfferResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "Offer Error",
                        Type = "Offer"
                    });
            }

            var response = JsonConvert.DeserializeObject<OfferResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<OfferResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<OfferResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<FetchInvoiceResponse>> FetchInvoiceAsync(FetchInvoiceRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.bolt12 };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.fetchinvoice, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"FetchInvoiceAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"FetchInvoiceAsync Response: {result.Content}");

            //check for error object  
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<FetchInvoiceResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"Paystatus Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<FetchInvoiceResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "Offer Error",
                        Type = "Offer"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<FetchInvoiceResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "Offer Error",
                        Type = "Offer"
                    });
            }

            var response = JsonConvert.DeserializeObject<FetchInvoiceResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<FetchInvoiceResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<FetchInvoiceResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<DecodeOfferResponse>> DecodeOfferAsync(DecodeOfferRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.bolt12 };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.decode, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"DecodeOfferAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"DecodeOfferAsync Response: {result.Content}");

            //check for error object  
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<DecodeOfferResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"DecodeOffer Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<DecodeOfferResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "DecodeOffer Error",
                        Type = "DecodeOffer"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<DecodeOfferResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "DecodeOffer Error",
                        Type = "DecodeOffer"
                    });
            }

            var response = JsonConvert.DeserializeObject<DecodeOfferResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<DecodeOfferResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<DecodeOfferResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<ListPaysResponse>> ListPaysAsync()
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.listpays, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"ListPays Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"ListPays Response: {result.Content}");



            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<ListPaysResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"ListPays Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<ListPaysResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "ListPays Error",
                        Type = "ListPays"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<ListPaysResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "ListPays Error",
                        Type = "ListPays"
                    });
            }

            var response = JsonConvert.DeserializeObject<ListPaysResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<ListPaysResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<ListPaysResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }

        public async Task<ResponseCLN<WithdrawResponse>> WithdrawAsync(WithdrawRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.ReceiveAddress, model.AmountInSats };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.withdraw, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"Withdraw Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"Withdraw Response: {result.Content}");

            //check for error object  
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                if (errorResponse != null)
                    return await Task.FromResult(new ResponseCLN<WithdrawResponse>
                    {
                        Message = errorResponse.Message,
                        Code = errorResponse.Code,
                        Name = errorResponse.Name,
                        Type = errorResponse.Type
                    });

                if (result.ErrorException != null)
                {
                    Log.Error(result.ErrorException, $"Withdraw Error:: {result.ErrorException.Message}");

                    //check for error object  
                    return await Task.FromResult(new ResponseCLN<WithdrawResponse>
                    {
                        Message = errorResponse.Message,
                        Code = 1,
                        Name = "Withdraw Error",
                        Type = "Withdraw"
                    });
                }

                else
                    return await Task.FromResult(new ResponseCLN<WithdrawResponse>
                    {
                        Message = "There was an error",
                        Code = 1,
                        Name = "Withdraw Error",
                        Type = "Withdraw"
                    });
            }

            var response = JsonConvert.DeserializeObject<WithdrawResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<WithdrawResponse>
                {
                    Message = "No response from API"
                });

            return await Task.FromResult(new ResponseCLN<WithdrawResponse>
            {
                Result = response,
                Message = "Request Successful",
            });
        }
    }
}
