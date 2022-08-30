using Bitcoin.Core.Interfaces;
using Bitcoin.Core.Models;
using Bitcoin.Core.Models.BitcoinCore;
using Bitcoin.Core.Models.CoreLightning;
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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<GetInfoResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<NewAddrResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<ListfundsResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<ListpeersResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

        public async Task<ResponseCLN<InvoiceResponse>> InvoiceAsync(InvoiceRequest model)
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.mSatoshi, model.labelReference, model.description, model.expiryInSeconds };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.invoice, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<InvoiceResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
                });
            }

            var response = JsonConvert.DeserializeObject<InvoiceResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseCLN<InvoiceResponse>
                {
                    Message = "No response from API"
                });

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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<ListInvoicesResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

            Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile(logopath));

            var fileName = $"{Guid.NewGuid()}.jpg";

            var fileFolder = "Docs/Invoice";

            var savePhotoPath = Path.Combine(
                       Directory.GetCurrentDirectory(), $"{fileFolder}", fileName);

            try
            {
                qrCodeImage.Save(savePhotoPath);
            }

            catch (Exception ex)
            {
                Log.Fatal(ex, $"Error {ex.Message}");
            }

            var request = _httpContextAccessor.HttpContext.Request;

            var filePath = $"{fileFolder}/{fileName}";
            var qrImgPath = $"{request.Scheme}://{request.Host}/{filePath}";

            return await Task.FromResult(new ResponseCLN<GenerateInvoiceQRCodeResponse>
            {
                Message = "Request Succesful",
                Result = new GenerateInvoiceQRCodeResponse
                {
                    ImageURL = qrImgPath,
                    Invoice = model.Invoice,
                    ImageBas64String = _utilityService.ConvertToBase64String(filePath)
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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<ConnectResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<FundChannelResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<ListNodesResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<ListChannelsResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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
            object[] @params = { model.bolt11 };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.pay, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<PayResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<DisconnectResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<DecodeInvoiceResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<DeleteInvoiceResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

        public  async Task<ResponseCLN<DelexpiredinvoiceResponse>> DelexpiredinvoiceAsync()
        {
            var client = new RestClient(_config["Lightning:Sparko:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new LNRPCRequest(LNRPCOperations.delexpiredinvoice, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<DelexpiredinvoiceResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<DecodePayResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //check for error object
                var errorResponse = JsonConvert.DeserializeObject<LNError>(result.Content);

                return await Task.FromResult(new ResponseCLN<PaystatusResponse>
                {
                    Message = errorResponse.Message,
                    Code = errorResponse.Code,
                    Name = errorResponse.Name,
                    Type = errorResponse.Type
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
    }
}
