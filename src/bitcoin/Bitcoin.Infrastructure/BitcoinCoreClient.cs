using Bitcoin.Core.Interfaces;
using Bitcoin.Core.Models;
using Bitcoin.Core.Models.BitcoinCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bitcoin.Infrastructure
{
    public class BitcoinCoreClient : IBitcoinCoreClient
    {
        private readonly IConfiguration _config;

        public BitcoinCoreClient(IConfiguration config)
        {
            this._config = config;
        }

        public async Task<ResponseBTC<string>> GetNewAddressAsync(GetNewAddressRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { model.label, model.address_type };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.getnewaddress, @params).WriteJSON(writer);
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
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.getbalance, @params).WriteJSON(writer);
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
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.getbalances, @params).WriteJSON(writer);
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
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { model.Address };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.getaddressinfo, @params).WriteJSON(writer);
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
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.listreceivedbyaddress, @params).WriteJSON(writer);
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
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { model.Address };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.getreceivedbyaddress, @params).WriteJSON(writer);
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
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { model.Address };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.validateaddress, @params).WriteJSON(writer);
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
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { model.TxId };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.gettransaction, @params).WriteJSON(writer);
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
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { model.TxId, model.n, model.include_mempool };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.gettxout, @params).WriteJSON(writer);
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
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.gettxoutsetinfo, @params).WriteJSON(writer);
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
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { model.NumberOfConfirmations };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.listunspent, @params).WriteJSON(writer);
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

            //if (!string.IsNullOrEmpty(model.Address))
            //    response.Result = response.Result.Where(x => x.address == model.Address).ToList();

            if (model.Addresses.Count > 0)
                response.Result = response.Result.Where(x => model.Addresses.Contains(x.address))
                    .ToList();


            return await Task.FromResult(response);
        }

        public async Task<ResponseBTC<GetBlockResponse>> GetBlockAsync(GetBlockRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { model.BlockHash };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.getblock, @params).WriteJSON(writer);
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
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { model.TxId, 1 };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.getrawtransaction, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

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
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

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
            new RPCRequest(RPCOperations.createrawtransaction, @params).WriteJSON(writer);
            writer.Flush();

            var body = RemoveQuotesFromString(writer.ToString()); 

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

            return await Task.FromResult(response);
        }


        public async Task<ResponseBTC<DecodeRawTransactionResponse>> DecodeRawTransactionAsync(DecodeRawTransactionRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { model.Hex };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.decoderawtransaction, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

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
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { model.Hex, JsonConvert.SerializeObject(model.SendAddressPrivateKeys) }; 

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.signrawtransactionwithkey, @params).WriteJSON(writer);
            writer.Flush();

            //to remopve unwanted characters in the string 
            var body = RemoveQuotesFromString(writer.ToString()); 

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

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


        private string RemoveQuotesFromString(string body)
        {
            body = body.Replace(@"\", "");
            body = body.Replace("\"[", "[");
            body = body.Replace("]\"", "]");

            return body;
        }
         
        public async Task<ResponseBTC<string>> SendRawTransactionAsync(SendRawTransactionRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { model.Hex };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.sendrawtransaction, @params).WriteJSON(writer);
            writer.Flush();
             
            var body = RemoveQuotesFromString(writer.ToString());

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

            return await Task.FromResult(response);
        }
         
        private async Task<ResponseBTC<string>> CreateRawTransactionAsync(CreateSignAndSendRawTransactionViaTxIdRequest model, string changeAddress, decimal changeAmount)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

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

            //add the change address
            jArrayReceive.Add(new JObject
                    {
                        { changeAddress, changeAmount }
                    });
             
            //the string manipulation as below may not be need if the objects are just added without JsonConvert.SerializeObject them
            object[] @params = { jArrayInput, jArrayReceive };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.createrawtransaction, @params).WriteJSON(writer);
            writer.Flush();

            var body = RemoveQuotesFromString(writer.ToString());

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var createRawRransactionResponse = JsonConvert.DeserializeObject<ResponseBTC<string>>(result.Content);

            if (createRawRransactionResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = createRawRransactionResponse.Error
                });

            return await Task.FromResult(createRawRransactionResponse);
        }

        public async Task<ResponseBTC<string>> CreateSignAndSendRawTransactAsync(CreateSignAndSendRawTransactionViaTxIdRequest model)
        {
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
            var unspentTransactions = listUnspentResponse.Result.Where(u => model.FromTransactions.Select(x=>x.txid).Contains(u.txid)).ToList();

            var totalUnspentAmount = unspentTransactions.Sum(t=>t.amount);

            if(totalUnspentAmount <= 0)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = "Declined - Insufficient balance "
                    }
                });

            var sendAmount = model.ToAddress.Amount;
            var changeAndMiningFeeAmount = totalUnspentAmount - sendAmount;

            //calculating fees
            //in * 146 + out * 33 + 10
            //in - number of inputs
            //out - number of outputs
            var txnFeeInBTC = ((model.FromTransactions.Count * 146) + (2 * 33) + 10)/ 100000000m; 
            var changeAmount = changeAndMiningFeeAmount - txnFeeInBTC;

            if (totalUnspentAmount < (sendAmount + changeAmount))
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = new BitcoinError
                    {
                        Message = $"Declined - Insufficient balance to pull of this transaction"
                    }
                });

            //create change address where the change will be dumped
            var createChangeNewAddressResponse = await GetNewAddressAsync(new GetNewAddressRequest
            { });

            if (createChangeNewAddressResponse.Error != null)
                return await Task.FromResult(new ResponseBTC<string>
                {
                    Error = createChangeNewAddressResponse.Error
                });
             
            //create the raw txn
            var createRawRransactionResponse = await CreateRawTransactionAsync(model, createChangeNewAddressResponse.Result, changeAmount);
              
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

        public async Task<ResponseBTC<string>> DumpPrivKeyAsync(DumpPrivKeyRequest model)
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = { model.Address };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.dumpprivkey, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();

            //body = body.Replace(@"\", "");
            //body = body.Replace("\"[", "[");
            //body = body.Replace("]\"", "]");

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

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

        public async Task<ResponseBTC<GetBlockchainInfoResponse>> GetBlockchainInfoAsync()
        {
            var client = new RestClient(_config["Bitcoin:URL"]);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Authorization", $"Basic {_config["Bitcoin:authKey"]}");
            request.AddHeader("Content-Type", "text/plain");

            //build the objects
            object[] @params = {   };

            var writer = new StringWriter();
            new RPCRequest(RPCOperations.getblockchaininfo, @params).WriteJSON(writer);
            writer.Flush();

            var body = writer.ToString();
             

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var result = await client.ExecuteAsync(request);

            var response = JsonConvert.DeserializeObject<ResponseBTC<GetBlockchainInfoResponse>>(result.Content);

            if(result.StatusCode != HttpStatusCode.OK)
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
    }
}
