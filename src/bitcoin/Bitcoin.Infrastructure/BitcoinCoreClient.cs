using Bitcoin.Core.Interfaces;
using Bitcoin.Core.Models;
using Bitcoin.Core.Models.BitcoinCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QRCoder;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Bitcoin.Core;
using Bitcoin.API.Services.L1;
using NBitcoin;
using System.Drawing.Imaging;
using Bitcoin.Core.Services;

namespace Bitcoin.Infrastructure
{
    public class BitcoinCoreClient : IBitcoinCoreClient
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UtilityService _utilityService;

        public BitcoinCoreClient(IConfiguration config,
            IHttpContextAccessor httpContextAccessor, UtilityService utilityService)
        {
            this._config = config;
            this._httpContextAccessor = httpContextAccessor;
            _utilityService = utilityService;
        }

        public async Task<ResponseBTC<string>> GetNewAddressAsync(GetNewAddressRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.label, model.address_type };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.getnewaddress, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //var body = rpcRequest.ToString().Replace("\r\n", "").Replace(@"\", "").Replace("rn", "");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<ResponseBTC<string>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            if (response.Result == null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<decimal>> GetBalanceAsync()
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.getbalance, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //var body = rpcRequest.ToString().Replace("\r\n", "").Replace(@"\", "").Replace("rn", "");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<ResponseBTC<decimal>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<decimal>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<GetBalancesResponse>> GetBalancesAsync()
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.getbalances, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<ResponseBTC<GetBalancesResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<GetBalancesResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<GetAddressInfoResponse>> GetAddressInfoAsync(GetAddressInfoRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Address };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.getaddressinfo, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<ResponseBTC<GetAddressInfoResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<GetAddressInfoResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<List<ListReceivedByAddressResponse>>> ListReceivedByAddressAsync()
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.listreceivedbyaddress, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<ResponseBTC<List<ListReceivedByAddressResponse>>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<List<ListReceivedByAddressResponse>>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<decimal>> GetReceivedByAddressAsync(GetReceivedByAddressRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Address };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.getreceivedbyaddress, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<ResponseBTC<decimal>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<decimal>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<Response<GetAddressInfo3rdPartyResponse>> GetAddressBalance3rdPartyAsync(GetAddressInfo3rdPartyRequest model)
        {
            //https://api.blockcypher.com/v1/btc/main/addrs/bc1qkm8jhsz9chkma8ye0zdys5w9rarj50usxe5plc/balance
            var url = $"{_config["Bitcoin:3rdParty:blockcypher:URL"]}v1/btc/main/addrs/{model.Address}/balance";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);

            Serilog.Log.Information($"GetAddressBalanceParty Response:: {JsonConvert.SerializeObject(model)}");
            var result = await client.ExecuteAsync(request);
            Serilog.Log.Information($"GetAddressBalanceParty Response:: {result.Content}");

            var response = JsonConvert.DeserializeObject<GetAddressInfo3rdPartyResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(
                    new Response<GetAddressInfo3rdPartyResponse>
                    {
                        Message = "No response from API"
                    });

            response.unit = "sat";
            return await Task.FromResult(
                new Response<GetAddressInfo3rdPartyResponse>
                {
                    Success = true,
                    Message = "Request Successful",
                    Data = response
                });
        }

        public async Task<ValidateResponseBTC<ValidateAddressResponse>> ValidateAddressAsync(ValidateAddressRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Address };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.validateaddress, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<ValidateResponseBTC<ValidateAddressResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ValidateResponseBTC<ValidateAddressResponse>
                {
                    Error = new ValidateBitcoinError
                    {
                        Error = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<Response<GetRecommendedFeeResponse>> GetRecommendedFeeAsync()
        {
            var url = $"{_config["Bitcoin:3rdParty:mempool:URL"]}api/v1/fees/recommended";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);

            var result = await client.ExecuteAsync(request);
            Serilog.Log.Information($"GetRecommendedFeeAsync Response:: {result.Content}");

            var response = JsonConvert.DeserializeObject<GetRecommendedFeeResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(
                    new Response<GetRecommendedFeeResponse>
                    {
                        Message = "No response from API"
                    });

            response.unit = "sat/vB";
            return await Task.FromResult(
                new Response<GetRecommendedFeeResponse>
                {
                    Success = true,
                    Message = "Request Successful",
                    Data = response
                });
        }

        public async Task<ResponseBTC<GetTransactionResponse>> GetTransactionAsync(GetTransactionRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.TxId };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.gettransaction, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<ResponseBTC<GetTransactionResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<GetTransactionResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        /// <summary>
        /// this only works for txns treated on the node.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBTC<GetTxOutResponse>> GetTxOutAsync(GetTxOutRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.TxId, model.n, model.include_mempool };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.gettxout, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<ResponseBTC<GetTxOutResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<GetTxOutResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<GetTxOutsetInfoResponse>> GetTxOutSetInfoAsync()
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.gettxoutsetinfo, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<ResponseBTC<GetTxOutsetInfoResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<GetTxOutsetInfoResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<List<ListUnspentResponse>>> ListUnspentAsync(ListUnspentRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.NumberOfConfirmations };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.listunspent, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<ResponseBTC<List<ListUnspentResponse>>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<List<ListUnspentResponse>>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            if (response.Error != null)
                return await Task.FromResult(new ResponseBTC<List<ListUnspentResponse>>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message
                    }
                });

            if (!string.IsNullOrEmpty(model.TxId))
                response.Result = response.Result.Where(x => x.txid == model.TxId)
                .ToList();

            if (model.Addresses != null && model.Addresses.Count > 0)
                response.Result = response.Result.Where(x => model.Addresses.Contains(x.address))
                    .ToList();

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<GetBlockResponse>> GetBlockAsync(GetBlockRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.BlockHash };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.getblock, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<ResponseBTC<GetBlockResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<GetBlockResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<Response<GetTransaction3rdPartyResponse>> GetTransaction3rdPartyAsync(GetTransaction3rdPartyRequest model)
        {
            var url = $"{_config["Bitcoin:3rdParty:qbit:URL"]}transactions/{model.TxId}?format=format";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);

            Serilog.Log.Information($"GetTransaction3rdParty Response:: {JsonConvert.SerializeObject(model)}");
            var result = await client.ExecuteAsync(request);
            Serilog.Log.Information($"GetTransaction3rdParty Response:: {result.Content}");

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                return await Task.FromResult(
                    new Response<GetTransaction3rdPartyResponse>
                    {
                        Message = result.StatusDescription
                    });

            var response = JsonConvert.DeserializeObject<GetTransaction3rdPartyResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(
                    new Response<GetTransaction3rdPartyResponse>
                    {
                        Message = "No response from API"
                    });

            return await Task.FromResult(
                new Response<GetTransaction3rdPartyResponse>
                {
                    Success = true,
                    Message = "Request Successful",
                    Data = response
                });
        }

        public async Task<Response<GetAssetPrizesResponse>> GetAssetPrizesAsync()
        {
            var url = $"{_config["Bitcoin:3rdParty:coinstats:URL"]}public/v1/coins/";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);

            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<GetAssetPrizesResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(
                    new Response<GetAssetPrizesResponse>
                    {
                        Message = "No response from API"
                    });

            return await Task.FromResult(
                new Response<GetAssetPrizesResponse>
                {
                    Success = true,
                    Message = "Request Successful",
                    Data = response
                });
        }

        public async Task<Response<GetAssetPrizeResponse>> GetAssetPrizeAsync(GetAssetPrizeRequest model)
        {
            var url = $"{_config["Bitcoin:3rdParty:coinstats:URL"]}public/v1/coins/{model.Code}";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);

            Serilog.Log.Information($"GetAssetPrizesAsync Response:: {JsonConvert.SerializeObject(model)}");
            var result = await client.ExecuteAsync(request);
            Serilog.Log.Information($"GetAssetPrizesAsync Response:: {result.Content}");

            if (result.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(result.Content))
                return await Task.FromResult(
                    new Response<GetAssetPrizeResponse>
                    {
                        Message = "No response from API"
                    });

            var response = JsonConvert.DeserializeObject<GetAssetPrizeResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(
                    new Response<GetAssetPrizeResponse>
                    {
                        Message = "No response from API"
                    });

            return await Task.FromResult(
                new Response<GetAssetPrizeResponse>
                {
                    Success = true,
                    Message = "Request Successful",
                    Data = response
                });
        }

        public async Task<Response<GetMempoolTransactionResponse>> GetMempoolTransactionAsync(GetMempoolTransactionRequest model)
        {
            var url = $"{_config["Bitcoin:3rdParty:mempool:URL"]}api/tx/{model.TxId}";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);

            var result = await client.ExecuteAsync(request);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                return await Task.FromResult(
                    new Response<GetMempoolTransactionResponse>
                    {
                        Message = result.StatusDescription
                    });

            var response = JsonConvert.DeserializeObject<GetMempoolTransactionResponse>(result.Content);

            if (response == null)
                return await Task.FromResult(
                    new Response<GetMempoolTransactionResponse>
                    {
                        Message = "No response from API"
                    });

            return await Task.FromResult(
                new Response<GetMempoolTransactionResponse>
                {
                    Success = true,
                    Message = "Request Successful",
                    Data = response
                });
        }

        public async Task<ResponseBTC<GetRawTransactionAsJSONResponse>> GetRawTransactionAsJSONAsync(GetRawTransactionAsJSONRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.TxId, 1 };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.getrawtransaction, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            Log.Information($"GetRawTransactionAsJSONAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"GetRawTransactionAsJSONAsync Response: {result.Content}");

            var response = JsonConvert.DeserializeObject<ResponseBTC<GetRawTransactionAsJSONResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<GetRawTransactionAsJSONResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<string>> CreateRawTransactionAsync(CreateRawTransactionRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects  
            JContainer jArrayInput = new JArray();
            foreach (var item in model.SendTransactions)
            {
                JObject jFromTx = new JObject
                {
                    { "txid", item.txid},
                    { "vout", item.vout }
                };
                jArrayInput.Add(jFromTx);
            }

            //build outputs  
            JContainer jArrayReceive = new JArray();
            foreach (var outputs in model.ReceiveAddresses)
            {
                JObject jToTx = new JObject
                    {
                        { outputs.Address, outputs.Amount }
                    };
                jArrayReceive.Add(jToTx);
            }

            //build the objects
            object[] @params = { jArrayInput, jArrayReceive };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.createrawtransaction, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());
             
            Log.Information($"CreateRawTransactionAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"CreateRawTransactionAsync Response: {result.Content}");

            var response = JsonConvert.DeserializeObject<ResponseBTC<string>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<DecodeRawTransactionResponse>> DecodeRawTransactionAsync(DecodeRawTransactionRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Hex };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.decoderawtransaction, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"DecodeRawTransactionAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"DecodeRawTransactionAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<DecodeRawTransactionResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<DecodeRawTransactionResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<SignRawTransactionWithKeysResponse>> SignRawTransactionAsync(SignRawTransactionWithKeysRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Hex, JsonConvert.SerializeObject(model.SendAddressPrivateKeys) };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.signrawtransactionwithkey, @params).WriteJSON(writer);
            writer.Flush();

            //to remopve unwanted characters in the string 
            var body = PruneAsJSONString(writer.ToString());
             
            Log.Information($"SignRawTransactionAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"SignRawTransactionAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<SignRawTransactionWithKeysResponse>>(result.Content);

            if (response.Error != null)
                return await Task.FromResult(new ResponseBTC<SignRawTransactionWithKeysResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            if (response.Result.errors.Count > 0)
                return await Task.FromResult(new ResponseBTC<SignRawTransactionWithKeysResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = $"Declined - There was an error in signning transaction. Kindly confirm the PrivateKeys are correct.",
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        /// <summary>
        /// mainly used when the param is a complex object hence adds some extra characters to the json string after conversion
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private string PruneAsJSONString(string body)
        {
            body = body.Replace(@"\", "");
            body = body.Replace("\"[", "[");
            body = body.Replace("]\"", "]");
            body = body.Replace("\"{", "{");
            body = body.Replace("}\"", "}");

            return body;
        }

        public async Task<ResponseBTC<string>> SendRawTransactionAsync(SendRawTransactionRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Hex };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.sendrawtransaction, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"SendRawTransactionAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"SendRawTransactionAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<string>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        private RestRequest CreateRestClientRequest()
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            return request;

        }
        private async Task<ResponseBTC<string>> CreateRawTransactionAsync(CreateSignAndSendRawTransactionViaTxIdRequest model,
            string changeAddress, decimal changeAmount)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build source objects  
            JContainer jArrayInput = new JArray();
            foreach (var item in model.FromTransactions)
            {
                JObject jFromTx = new JObject
                {
                    { "txid", item.txid},
                    { "vout", item.vout }
                };
                jArrayInput.Add(jFromTx);
            }

            //build outputs  
            JContainer jArrayReceive = new JArray();

            //add the receive address and change add.
            jArrayReceive.Add(new JObject
                    {
                        { model.ToAddress.Address , model.ToAddress.Amount },
                    });

            //if change address is not specifired, dont include it. Only include if its defined
            if (!string.IsNullOrEmpty(changeAddress) && changeAmount > 0)
                //add the change address
                jArrayReceive.Add(new JObject
                    {
                        { changeAddress, changeAmount }
                    });

            //the string manipulation as below may not be need if the objects are just added without JsonConvert.SerializeObject them
            object[] @params = { jArrayInput, jArrayReceive };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.createrawtransaction, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"CreateRawTransactionAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"CreateRawTransactionAsync Response: {result.Content}");


            var createRawRransactionResponse = JsonConvert.DeserializeObject<ResponseBTC<string>>(result.Content);

            if (createRawRransactionResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = createRawRransactionResponse.Error
                });

            return await Task.FromResult(createRawRransactionResponse);
        }

        public async Task<ResponseBTC<string>> CreateSignAndSendRawTransactionAsync(CreateSignAndSendRawTransactionViaTxIdRequest model)
        {
            if (string.IsNullOrEmpty(model.FeeType))
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = "Kindly indicate who is to pay for the transaction fees, sender or receiver"
                    }
                });

            //validate receive address
            var validateAddressResponse = await ValidateAddressAsync(new ValidateAddressRequest
            {
                Address = model.ToAddress.Address
            });

            if (validateAddressResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = validateAddressResponse.Error.Error
                    }
                });

            List<string> sendAddresses = new List<string>();

            //get the transactions tied to this id
            foreach (var item in model.FromTransactions)
            {
                var getRawTransactionResponse = await GetRawTransactionAsJSONAsync(new GetRawTransactionAsJSONRequest
                {
                    TxId = item.txid
                });

                //if any error
                if (getRawTransactionResponse.Error != null)
                    return await Task.FromResult(new ResponseBTC<string>
                    {
                        Error = getRawTransactionResponse.Error
                    });

                //get the address at the index of the txn to confirm if it exists
                var getRawJSONTransactionResponseVout = getRawTransactionResponse.Result.vout.FirstOrDefault(x => x.n == item.vout);

                if (getRawJSONTransactionResponseVout == null)
                    return await Task.FromResult(new ResponseBTC<string>
                    {
                        Error = new BitcoinError
                        {
                            Message = $"There is no transaction on txid {item.txid} with index {item.vout}"
                        }
                    });

                sendAddresses.Add(getRawJSONTransactionResponseVout.scriptPubKey.address);
            }

            //get all unsoent txns on your node and filter by addresses you want to send from
            var listUnspentResponse = await ListUnspentAsync(new ListUnspentRequest
            {
                Addresses = sendAddresses,
                NumberOfConfirmations = 1
            });

            if (listUnspentResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = listUnspentResponse.Error
                });

            var unspentAmount = 0m;

            if (listUnspentResponse.Result.Count > 0)
            {
                //to get all the txns
                var unspentTransactions = listUnspentResponse.Result.Where(u => model.FromTransactions
                .Select(x => x.txid).Contains(u.txid)).ToList();

                unspentAmount = unspentTransactions.Sum(t => t.amount);

                if (unspentAmount <= 0 || model.ToAddress.Amount > unspentAmount)
                    return await Task.FromResult(new ResponseBTC<string>
                    {
                        Error = new BitcoinError
                        {
                            Message = "Declined - Insufficient balance "
                        }
                    });
            }

            else//if (listUnspentResponse.Result.Count <= 0)
            {
                unspentAmount = model.ToAddress.Amount + (model.Fees.HasValue ? model.Fees.Value : 0m); 
            }
             
            //calculating fees
            //in * 146 + out * 33 + 10
            //in - number of inputs
            //out - number of outputs
            var fee = model.Fees.HasValue ? model.Fees.Value : ((model.FromTransactions.Count * 146) + (2 * 33) + 10) / 100000000m;

            var changeBalance = unspentAmount - (model.ToAddress.Amount + fee);

            //balance cannot cater for fees
            //if (unspentAmount < (model.ToAddress.Amount + fee)) //if the bala
            if (changeBalance < 0) //if cannot handle the fee, the fee should be taken from the receieve amount
            {
                model.ToAddress.Amount = model.ToAddress.Amount - fee;
                changeBalance = 0m;
            }

            if (model.ToAddress.Amount < 0)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = $"Declined - Error processing the payment."
                    }
                });
             
            ResponseBTC<string> createRawChangeAddressResponse = null;

            var changeAdress = model.ChangeAddress;
            if (changeBalance > 0 && string.IsNullOrEmpty(changeAdress)) //there is no change address so create change address
            {
                //create change address where the change will be dumped
                createRawChangeAddressResponse = await GetRawChangeAddressAsync(new GetRawChangeAddressRequest
                {
                });

                if (createRawChangeAddressResponse.Error != null)
                    return await Task.FromResult(new ResponseBTC<string>
                    {
                        Error = createRawChangeAddressResponse.Error
                    });

                changeAdress = createRawChangeAddressResponse.Result;
            }

            //create the raw txn
            var createRawRransactionResponse = await CreateRawTransactionAsync(model, changeAdress, changeBalance);

            if (createRawRransactionResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = createRawRransactionResponse.Error
                });

            //sign the raw txn
            var signTransactionResponse = await SignRawTransactionAsync(new SignRawTransactionWithKeysRequest
            {
                Hex = createRawRransactionResponse.Result,
                SendAddressPrivateKeys = model.FromTransactions.Select(x => x.addressPrivateKey).ToList()
            });

            if (signTransactionResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = signTransactionResponse.Error
                });

            //send txn
            var sendRawTransactionResponse = await SendRawTransactionAsync(new SendRawTransactionRequest
            {
                Hex = signTransactionResponse.Result.hex
            });

            if (sendRawTransactionResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = sendRawTransactionResponse.Error
                });

            return await Task.FromResult(sendRawTransactionResponse);
        }

        public async Task<ResponseBTC<string>> CreateSignAndSendRawNodeTransactionAsync(CreateSignAndSendRawTransactionViaTxIdRequest model)
        {
            if (string.IsNullOrEmpty(model.FeeType))
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = "Kindly indicate who is to pay for the transaction fees, sender or receiver"
                    }
                });

            //validate receive address
            var validateAddressResponse = await ValidateAddressAsync(new ValidateAddressRequest
            {
                Address = model.ToAddress.Address
            });

            if (validateAddressResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = validateAddressResponse.Error.Error
                    }
                });

            List<string> sendAddresses = new List<string>();

            //get the transactions tied to this id
            foreach (var item in model.FromTransactions)
            {
                var getRawTransactionResponse = await GetRawTransactionAsJSONAsync(new GetRawTransactionAsJSONRequest
                {
                    TxId = item.txid
                });

                //if any error
                if (getRawTransactionResponse.Error != null)
                    return await Task.FromResult(new ResponseBTC<string>
                    {
                        Error = getRawTransactionResponse.Error
                    });

                //get the address at the index of the txn to confirm if it exists
                var getRawJSONTransactionResponseVout = getRawTransactionResponse.Result.vout.FirstOrDefault(x => x.n == item.vout);

                if (getRawJSONTransactionResponseVout == null)
                    return await Task.FromResult(new ResponseBTC<string>
                    {
                        Error = new BitcoinError
                        {
                            Message = $"There is no transaction on txid {item.txid} with index {item.vout}"
                        }
                    });

                sendAddresses.Add(getRawJSONTransactionResponseVout.scriptPubKey.address);
            }

            //get all unsoent txns on your node and filter by addresses you want to send from
            var listUnspentResponse = await ListUnspentAsync(new ListUnspentRequest
            {
                Addresses = sendAddresses,
                NumberOfConfirmations = 1
            });

            if (listUnspentResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = listUnspentResponse.Error
                });

            if (listUnspentResponse.Result.Count <= 0)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = "Declined - Insufficient funds in address"
                    }
                });

            //to get all the txns
            var unspentTransactions = listUnspentResponse.Result.Where(u => model.FromTransactions.Select(x => x.txid).Contains(u.txid)).ToList();

            var unspentAmount = unspentTransactions.Sum(t => t.amount);

            if (unspentAmount <= 0 || model.ToAddress.Amount > unspentAmount)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = "Declined - Insufficient balance "
                    }
                });

            //calculating fees
            //in * 146 + out * 33 + 10
            //in - number of inputs
            //out - number of outputs
            var fee = model.Fees.HasValue ? model.Fees.Value : ((model.FromTransactions.Count * 146) + (2 * 33) + 10) / 100000000m;

            var changeBalance = unspentAmount - (model.ToAddress.Amount + fee);

            //balance cannot cater for fees
            //if (unspentAmount < (model.ToAddress.Amount + fee)) //if the bala
            if (changeBalance < 0) //if cannot handle the fee, the fee should be taken from the receieve amount
            {
                model.ToAddress.Amount = model.ToAddress.Amount - fee;
                changeBalance = 0m;
            }

            if (model.ToAddress.Amount < 0)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = $"Declined - Error processing the payment."
                    }
                });

            //var changeAmount = changeValue;

            //if (changeValue == 0) //he is spending everything in the account //12 6 1
            //    changeAmount = 0m; //no change, the person wants to spend everything in the wallet

            //else //he is not spending everything in the account 
            //    changeAmount = unspentAmount - (model.ToAddress.Amount + fee);

            //if (changeAmount < 0)
            //    return await Task.FromResult(new ResponseBTC<string>
            //    {
            //        Error = new BitcoinError
            //        {
            //            Message = $"Declined - Insufficient balance to handle transaction fees."
            //        }
            //    });

            //if (changeAmount == 0 && model.FeeType == Constant.FEE_TYPE_SENDER_PAYS)
            //    return await Task.FromResult(new ResponseBTC<string>
            //    {
            //        Error = new BitcoinError
            //        {
            //            Message = $"Declined - Insufficient balance to handle transaction fees as the feetype indicated is sender."
            //        }
            //    });

            //if (changeAmount == 0 && model.FeeType == Constant.FEE_TYPE_RECEIVER_PAYS)
            //    model.ToAddress.Amount = model.ToAddress.Amount - fee;

            ResponseBTC<string> createRawChangeAddressResponse = null;

            var changeAdress = model.ChangeAddress;
            if (changeBalance > 0 && string.IsNullOrEmpty(changeAdress)) //there is change so create change address
            {
                //create change address where the change will be dumped
                createRawChangeAddressResponse = await GetRawChangeAddressAsync(new GetRawChangeAddressRequest
                {
                });

                if (createRawChangeAddressResponse.Error != null)
                    return await Task.FromResult(new ResponseBTC<string>
                    {
                        Error = createRawChangeAddressResponse.Error
                    });

                changeAdress = createRawChangeAddressResponse.Result;
            }

            //create the raw txn
            var createRawRransactionResponse = await CreateRawTransactionAsync(model, changeAdress, changeBalance);

            if (createRawRransactionResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = createRawRransactionResponse.Error
                });

            //sign the raw txn
            var signTransactionResponse = await SignRawTransactionAsync(new SignRawTransactionWithKeysRequest
            {
                Hex = createRawRransactionResponse.Result,
                SendAddressPrivateKeys = model.FromTransactions.Select(x => x.addressPrivateKey).ToList()
            });

            if (signTransactionResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = signTransactionResponse.Error
                });

            //send txn
            var sendRawTransactionResponse = await SendRawTransactionAsync(new SendRawTransactionRequest
            {
                Hex = signTransactionResponse.Result.hex
            });

            if (sendRawTransactionResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = sendRawTransactionResponse.Error
                });

            return await Task.FromResult(sendRawTransactionResponse);
        }


        public async Task<ResponseBTC<GetBlockchainInfoResponse>> GetBlockchainInfoAsync()
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.getblockchaininfo, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();


            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"GetBlockchainInfoAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"GetBlockchainInfoAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<GetBlockchainInfoResponse>>(result.Content);

            if (result.StatusCode != HttpStatusCode.OK)
                return await Task.FromResult(new ResponseBTC<GetBlockchainInfoResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = result.StatusDescription,
                    }
                });

            if (response.Error != null)
                return await Task.FromResult(new ResponseBTC<GetBlockchainInfoResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ListWalletsResponse> ListWalletsAsync()
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.listwallets, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();


            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"ListWalletsAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"ListWalletsAsync Response: {result.Content}");



            var response = JsonConvert.DeserializeObject<ListWalletsResponse>(result.Content);

            if (response.Error != null)
                return await Task.FromResult(new ListWalletsResponse
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<LoadWalletResponse>> LoadWalletAsync(LoadWalletRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Name };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.loadwallet, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();


            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"ListWalletsAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"ListWalletsAsync Response: {result.Content}");

            var response = JsonConvert.DeserializeObject<ResponseBTC<LoadWalletResponse>>(result.Content);

            if (response.Error != null)
                return await Task.FromResult(new ResponseBTC<LoadWalletResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        private Response<string> GenerateQRCodeAsync(string address, string qrLogo)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                    QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(address, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qRCodeData);

                    // logo path
                    var logopath = Path.Combine(
                               Directory.GetCurrentDirectory(), $"{"Docs/QR"}", qrLogo);

                    Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Orange, Color.Black, (Bitmap)Bitmap.FromFile(logopath));

                    using (Bitmap bitmap = qrCode.GetGraphic(20))
                    {
                        Log.Information("About to save file stream");

                        bitmap.Save(memoryStream, ImageFormat.Png);

                        Log.Information("Saved file stream");

                        return new Response<string>
                        {
                            Success = true,
                            Message = "Request Successful",
                            Data = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray())
                        };
                    }
                }
            }

            catch (Exception ex)
            {
                Log.Information("Error saving file stream");

                Log.Error(ex, $"QRCode generation {ex.Message}");

                return new Response<string>
                {
                    Message = "Error generating QR Code",
                };
            }
        }

        public async Task<Response<GenerateAddressQRCodeResponse>> GenerateAddressQRCodeAsync(GenerateAddressQRCodeRequest model)
        {
            var validateAddressResponse = await ValidateAddressAsync(new ValidateAddressRequest
            {
                Address = model.Address
            });

            if (validateAddressResponse.Error != null)
                return await Task.FromResult(new Response<GenerateAddressQRCodeResponse>
                {
                    Message = validateAddressResponse.Error.Error
                });

            //var logopath = Path.Combine(
            //           Directory.GetCurrentDirectory(), $"{"Docs/QR"}", "btclogo.PNG");

            //var generateQRCodeResponse = GenerateQRCodeAsync(model.Address, logopath);

            //if(!generateQRCodeResponse.Success)
            //    return await Task.FromResult(new Response<GenerateAddressQRCodeResponse>
            //    {
            //        Message = generateQRCodeResponse.Message
            //    });

            // logo path
            var logopath = Path.Combine(
                       Directory.GetCurrentDirectory(), $"{"Docs/QR"}", "btclogo.JPG");

            var generateQRCodeResponse = _utilityService.GenerateQRCodeAsync(model.Address, logopath);

            if (!generateQRCodeResponse.Success)
                return await Task.FromResult(new Response<GenerateAddressQRCodeResponse>
                {
                    Message = generateQRCodeResponse.Message
                });

            return await Task.FromResult(new Response<GenerateAddressQRCodeResponse>
            {
                Success = true,
                Message = "Request Succesful",
                Data = new GenerateAddressQRCodeResponse
                {
                    ImageURL = string.Empty,
                    Address = model.Address,
                    ImageBas64String = generateQRCodeResponse.Data
                }
            });
        }

        private string ConvertToBase64String(string imagePath)
        {
            using (Image image = Image.FromFile(imagePath))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        public async Task<ResponseBTC<AbandonTransactionResponse>> AbandonTransactionAsync(AbandonTransactionRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.TxId };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.abandontransaction, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"AbandonTransactionAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"AbandonTransactionAsync Response: {result.Content}");

            var response = JsonConvert.DeserializeObject<ResponseBTC<AbandonTransactionResponse>>(result.Content);

            if (response.Error != null)
                return await Task.FromResult(new ResponseBTC<AbandonTransactionResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<string>> DumpPrivKeyAsync(DumpPrivKeyRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Address };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.dumpprivkey, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"DumpPrivKeyAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"DumpPrivKeyAsync Response: {result.Content}");

            var response = JsonConvert.DeserializeObject<ResponseBTC<string>>(result.Content);

            if (response.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<CreateWalletResponse>> CreateWalletAsync(CreateWalletRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Name };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.createwallet, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"CreateWalletAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"CreateWalletAsync Response: {result.Content}");
             
            var response = JsonConvert.DeserializeObject<ResponseBTC<CreateWalletResponse>>(result.Content);

            if (response.Error != null)
                return await Task.FromResult(new ResponseBTC<CreateWalletResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<DumpWalletResponse>> DumpWalletAsync(DumpWalletRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.FileName };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.dumpwallet, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"DumpWalletAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"DumpWalletAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<DumpWalletResponse>>(result.Content);

            if (response.Error != null)
                return await Task.FromResult(new ResponseBTC<DumpWalletResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<BumpFeeResponse>> BumpFeeAsync(BumpFeeRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.TxId };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.bumpfee, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"BumpFeeAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"BumpFeeAsync Response: {result.Content}");



            var response = JsonConvert.DeserializeObject<ResponseBTC<BumpFeeResponse>>(result.Content);

            if (response.Error != null)
                return await Task.FromResult(new ResponseBTC<BumpFeeResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<GetWalletInfoResponse>> GetWalletInfoAsync()
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.getwalletinfo, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);


            Log.Information($"GetWalletInfoAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"GetWalletInfoAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<GetWalletInfoResponse>>(result.Content);

            if (response.Error != null)
                return await Task.FromResult(new ResponseBTC<GetWalletInfoResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<UnloadWalletResponse>> UnloadwalletAsync(UnloadWalletRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Name };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.unloadwallet, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"UnloadwalletAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"UnloadwalletAsync Response: {result.Content}");



            var response = JsonConvert.DeserializeObject<ResponseBTC<UnloadWalletResponse>>(result.Content);

            if (response.Error != null)
                return await Task.FromResult(new ResponseBTC<UnloadWalletResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<ListWalletDirResponse>> ListWalletDirAsync()
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.listwalletdir, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"ListWalletDirAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"ListWalletDirAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<ListWalletDirResponse>>(result.Content);

            if (response.Error != null)
                return await Task.FromResult(new ResponseBTC<ListWalletDirResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<string>> GetRawChangeAddressAsync(GetRawChangeAddressRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.address_type };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.getrawchangeaddress, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"GetRawChangeAddressAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"GetRawChangeAddressAsync Response: {result.Content}");

            var response = JsonConvert.DeserializeObject<ResponseBTC<string>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            if (response.Result == null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<BackupWalletResponse>> BackupWalletAsync(BackupWalletRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Destination };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.backupwallet, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"BackupWalletAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"BackupWalletAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<BackupWalletResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<BackupWalletResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            if (response.Result == null)
                return await Task.FromResult(new ResponseBTC<BackupWalletResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<WalletlockResponse>> WalletlockAsync(WalletlockRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Passphrase };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.walletlock, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"WalletlockAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"WalletlockAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<WalletlockResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<WalletlockResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            if (response.Result == null)
                return await Task.FromResult(new ResponseBTC<WalletlockResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<WalletPassphraseResponse>> WalletPassphraseAsync(WalletPassphraseRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.PassPhrase, model.Timeout };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.walletpassphrase, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"WalletPassphraseAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"WalletPassphraseAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<WalletPassphraseResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<WalletPassphraseResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            if (response.Result == null)
                return await Task.FromResult(new ResponseBTC<WalletPassphraseResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<string>> SignMessageAsync(SignMessageRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Address, model.Message };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.signmessage, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"SignMessageAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"SignMessageAsync Response: {result.Content}");



            var response = JsonConvert.DeserializeObject<ResponseBTC<string>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            if (response.Result == null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = response.Error.Message,
                        Code = response.Error.Code
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<bool>> VerifySignedMessageAsync(VerifySignedMessageRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Address, model.Signature, model.Message };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.verifymessage, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"VerifySignedMessageAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"VerifySignedMessageAsync Response: {result.Content}");



            var response = JsonConvert.DeserializeObject<ResponseBTC<bool>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<bool>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<FundRawtransactionResponse>> FundRawtransactionAsync(FundRawtransactionRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.HexString, model.options };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.fundrawtransaction, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"FundRawtransactionAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"FundRawtransactionAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<FundRawtransactionResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<FundRawtransactionResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<WalletCreateFundedPsbtResponse>> WalletCreateFundedPSBTAsync(WalletCreateFundedPsbtRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects  
            JContainer jArrayInput = new JArray();
            foreach (var item in model.Inputs)
            {
                JObject jFromTx = new JObject
                {
                    { "txid", item.txid},
                    { "vout", item.vout }
                };
                jArrayInput.Add(jFromTx);
            }

            //build outputs  
            JContainer jArrayReceive = new JArray();
            foreach (var outputs in model.Outputs)
            {
                //add address info
                JObject jToTxAddressAmount = new JObject
                    {
                        { outputs.Address, outputs.Amount }
                    };
                jArrayReceive.Add(jToTxAddressAmount);

                ////add data
                //JObject jToTxData = new JObject
                //    {
                //        { "data", "" }
                //    };
                //jArrayReceive.Add(jToTxData);
            }

            //build the objects
            //object[] @params = { jArrayInput, jArrayReceive, model.Locktime, model.Options };
            //object[] @params = { jArrayInput, jArrayReceive, model.Locktime, JsonConvert.SerializeObject(model.Options), model.Bip32derivs };
            object[] @params = { jArrayInput, jArrayReceive, model.Locktime, JsonConvert.SerializeObject(model.Options), model.Bip32derivs };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.walletcreatefundedpsbt, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"WalletCreateFundedPSBTAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"WalletCreateFundedPSBTAsync Response: {result.Content}");
             
            var response = JsonConvert.DeserializeObject<ResponseBTC<WalletCreateFundedPsbtResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<WalletCreateFundedPsbtResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<DecodePsbtResponse>> DecodePSBTAsync(DecodePsbtRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Hex };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.decodepsbt, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"DecodePSBTAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"DecodePSBTAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<DecodePsbtResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<DecodePsbtResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<CreateMultiSigResponse>> CreateMultisigAsync(CreateMultisigRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.nrequired, JsonConvert.SerializeObject(model.PublicKeys.ToArray()), model.address_type };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.createmultisig, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"CreateMultisigAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"CreateMultisigAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<CreateMultiSigResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<CreateMultiSigResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<CreateMultisigAddressResponse>> CreateMultiSigAddressAsync(CreateMultisigAddressRequest model)
        {
            if (model.Signatories <= 0)
                return await Task.FromResult(new ResponseBTC<CreateMultisigAddressResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "Declined - Specify the number of signatories to the address."
                    }
                });

            if (model.Signatories < model.SignatoriesToApprove)
                return await Task.FromResult(new ResponseBTC<CreateMultisigAddressResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "Declined - Required Signatories to approve transaction must be less than or equal to account signatories."
                    }
                });

            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //create n number of addresses
            var response = new CreateMultisigAddressResponse
            {
                NAddressesInfo = new List<AddressInfo>()
            };

            for (int i = 0; i < model.Signatories; i++)
            {
                //create an address
                var getNewAddressResponse = await GetNewAddressAsync(new GetNewAddressRequest
                {
                    address_type = model.address_type
                });

                if (getNewAddressResponse.Error != null)
                    return await Task.FromResult(new ResponseBTC<CreateMultisigAddressResponse>
                    {
                        Error = new BitcoinError
                        {
                            Message = getNewAddressResponse.Error.Message
                        }
                    });

                //get the public key of the address created
                var getAddressInfo = await GetAddressInfoAsync(new GetAddressInfoRequest
                {
                    Address = getNewAddressResponse.Result
                });

                if (getAddressInfo.Error != null)
                    return await Task.FromResult(new ResponseBTC<CreateMultisigAddressResponse>
                    {
                        Error = new BitcoinError
                        {
                            Message = getAddressInfo.Error.Message
                        }
                    });

                response.NAddressesInfo.Add(new AddressInfo
                {
                    Address = getNewAddressResponse.Result,
                    Pubkey = getAddressInfo.Result.pubkey
                });
            }

            var addressPublicKeys = response.NAddressesInfo.Select(x => x.Pubkey).ToArray();

            //build the objects
            object[] @params = { model.SignatoriesToApprove, JsonConvert.SerializeObject(addressPublicKeys), model.address_type };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.createmultisig, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"CreateMultiSigAddressAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"CreateMultiSigAddressAsync Response: {result.Content}");



            var multisigAddressResult = JsonConvert.DeserializeObject<ResponseBTC<MultiSigAddress>>(result.Content);

            if (multisigAddressResult == null)
                return await Task.FromResult(new ResponseBTC<CreateMultisigAddressResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            if (multisigAddressResult.Error != null)
                return await Task.FromResult(new ResponseBTC<CreateMultisigAddressResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = multisigAddressResult.Error.Message
                    }
                });

            response.MultisigAddress = multisigAddressResult.Result;

            return await Task.FromResult(new ResponseBTC<CreateMultisigAddressResponse>
            {
                Result = response,
                Id = multisigAddressResult.Id,
                Error = multisigAddressResult.Error
            });
        }

        public async Task<ResponseBTC<string>> CreatePsbtAsync(CreatePsbtRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects  
            JContainer jArrayInput = new JArray();
            foreach (var item in model.Inputs)
            {
                JObject jFromTx = new JObject
                {
                    { "txid", item.txid},
                    { "vout", item.vout }
                };
                jArrayInput.Add(jFromTx);
            }

            //build outputs  
            JContainer jArrayReceive = new JArray();
            foreach (var outputs in model.Outputs)
            {
                //add address info
                JObject jToTxAddressAmount = new JObject
                    {
                        { outputs.Address, outputs.Amount }
                    };
                jArrayReceive.Add(jToTxAddressAmount);
            }

            object[] @params = { jArrayInput, jArrayReceive, model.Locktime, model.Replaceable };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.createpsbt, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"CreatePsbtAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"CreatePsbtAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<string>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<WalletProcessPsbtResponse>> WalletProcessPsbtAsync(WalletProcessPsbtRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Psbt };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.walletprocesspsbt, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"WalletProcessPsbtAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"WalletProcessPsbtAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<WalletProcessPsbtResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<WalletProcessPsbtResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<FinalizePsbtResponse>> FinalizePsbtAsync(FinalizePsbtRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Psbt };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.finalizepsbt, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"FinalizePsbtAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"FinalizePsbtAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<FinalizePsbtResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<FinalizePsbtResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<string>> UtxoUpdatePsbtAsync(UtxoUpdatePsbtRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Psbt };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.utxoupdatepsbt, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"UtxoUpdatePsbtAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"UtxoUpdatePsbtAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<string>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<ConvertToPsbtResponse>> ConvertToPsbtAsync(ConvertToPsbtRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.hexstring, model.permitsigdata, model.iswitness };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.convertopsbt, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"ConvertToPsbtAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"ConvertToPsbtAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<ConvertToPsbtResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<ConvertToPsbtResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<AddMultisigAddressResponse>> AddMultisigAddressAsync(AddMultisigAddressRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Signatories, JsonConvert.SerializeObject(model.PublicKeysOrAddresses.ToArray()), model.label, model.address_type };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.addmultisigaddress, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"AddMultisigAddressAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"AddMultisigAddressAsync Response: {result.Content}");
             
            var response = JsonConvert.DeserializeObject<ResponseBTC<AddMultisigAddressResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<AddMultisigAddressResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<ImportAddressResponse>> ImportAddressAsync(ImportAddressRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.address, model.label, };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.importaddress, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"ImportAddressAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"ImportAddressAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<ImportAddressResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<ImportAddressResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<CombinePsbResponse>> CombinePsbtAsync(CombinePsbtRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { JsonConvert.SerializeObject(model.Psbts) };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.combinepsbt, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"CombinePsbtAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"CombinePsbtAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<CombinePsbResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<CombinePsbResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<AnalyzePsbtResponse>> AnalyzePsbtAsync(AnalyzePsbtRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { JsonConvert.SerializeObject(model.Psbt) };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.analyzepsbt, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"AnalyzePsbtAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"AnalyzePsbtAsync Response: {result.Content}");
             
            var response = JsonConvert.DeserializeObject<ResponseBTC<AnalyzePsbtResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<AnalyzePsbtResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<DecodeScriptResponse>> DecodeScriptAsync(DecodeScriptRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.ReceemScript };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.decodescript, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"DecodeScriptAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"DecodeScriptAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<DecodeScriptResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<DecodeScriptResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<DeriveAddressesResponse>> DeriveAddressesAsync(DeriveAddressesRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.Descriptor };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.deriveaddresses, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"DeriveAddressesAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"DeriveAddressesAsync Response: {result.Content}");



            var response = JsonConvert.DeserializeObject<ResponseBTC<DeriveAddressesResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<DeriveAddressesResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<GetDescriptorinfoResponse>> GetDescriptorinfoAsync(GetDescriptorinfoRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.descriptor };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.getdescriptorinfo, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"GetDescriptorinfoAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"GetDescriptorinfoAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<GetDescriptorinfoResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<GetDescriptorinfoResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<List<string>>> GenerateBlockAsync(GenerateBlockRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.NumberOfBlocks };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.generate, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"GenerateBlockAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"GenerateBlockAsync Response: {result.Content}");


            var response = JsonConvert.DeserializeObject<ResponseBTC<List<string>>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<List<string>>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<EstimatesMartfeeResponse>> EstimatesMartfeeAsync(EstimatesMartfeeRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = CreateRestClientRequest();

            //build the objects
            object[] @params = { model.conf_target, model.estimate_mode };

            var writer = new StringWriter();
            new BTCRPCRequest(BTCRPCOperations.estimatesmartfee, @params).WriteJSON(writer);
            writer.Flush();

            var body = PruneAsJSONString(writer.ToString());

            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //var result = await client.ExecuteAsync(request);

            Log.Information($"EstimatesMartfeeAsync Request: {body}");
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);
            Log.Information($"EstimatesMartfeeAsync Response: {result.Content}");



            var response = JsonConvert.DeserializeObject<ResponseBTC<EstimatesMartfeeResponse>>(result.Content);

            if (response == null)
                return await Task.FromResult(new ResponseBTC<EstimatesMartfeeResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = "No response from API"
                    }
                });

            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<GetNewAddressQRCodeResponse>> GetNewAddressQRCodeAsync(GetNewAddressQRCodeRequest model)
        {
            string address = model.Address;
            if (string.IsNullOrEmpty(model.Address))
            {
                var generateNewAddressResponse = await GetNewAddressAsync(new GetNewAddressRequest
                {
                    address_type = model.address_type
                });

                if (generateNewAddressResponse.Error != null)
                    return await Task.FromResult(new ResponseBTC<GetNewAddressQRCodeResponse>
                    {
                        Error = generateNewAddressResponse.Error
                    });

                address = generateNewAddressResponse.Result;
            }

            //generate QR code
            var qrCodeResponse = await GenerateAddressQRCodeAsync(new GenerateAddressQRCodeRequest
            {
                Address = address
            });

            if (!qrCodeResponse.Success)
                return await Task.FromResult(new ResponseBTC<GetNewAddressQRCodeResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = qrCodeResponse.Message,
                    }
                });

            var dumpPrivKeyResponse = await DumpPrivKeyAsync(new DumpPrivKeyRequest
            {
                Address = address
            });

            //include the priv key
            if (dumpPrivKeyResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<GetNewAddressQRCodeResponse>
                {
                    Error = new BitcoinError
                    {
                        Message = dumpPrivKeyResponse.Error.Message,
                    }
                });

            return await Task.FromResult(new ResponseBTC<GetNewAddressQRCodeResponse>
            {
                Result = new GetNewAddressQRCodeResponse
                {
                    Address = qrCodeResponse.Data.Address,
                    ImageBas64String = qrCodeResponse.Data.ImageBas64String,
                    ImageURL = qrCodeResponse.Data.ImageURL,
                    PriKey = dumpPrivKeyResponse.Result
                },
            });
        }

        public async Task<Response<string>> GetAddressFromXPUBAsync(GetAddressFromXPUBKeyRequest model)
        {
            var pubkey = ExtPubKey.Parse(model.XPubKey, Network.Main);
            var newAddress = pubkey.Derive(0).Derive(model.Index).PubKey.GetAddress(ScriptPubKeyType.Segwit, Network.Main);

            return await Task.FromResult(new Response<string>
            {
                Success = true,
                Message = "Request Successful",
                Data = newAddress.ToString()
            });
        }

        public async Task<Response<decimal>> GetUnspentBalanceAsync(GetUnspentBalanceRequest model)
        {
            var listUnspentResponse = await ListUnspentAsync(model);

            if (listUnspentResponse.Error != null)
                return await Task.FromResult(new Response<decimal>
                {
                    Message = listUnspentResponse.Error.Message
                });

            return await Task.FromResult(new Response<decimal>
            {
                Message = "Request Successful",
                Success = true,
                Data = listUnspentResponse.Result.Sum(x => x.amount)
            });
        }
    }
}
