using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bitcoin.Core.Models.BitcoinCore
{
    public class GetNewAddressRequest : RequestBTC
    {
        public string address_type { get; set; }
        public string label { get; set; }
    }


    //from rpcserver.h
    public enum RPCOperations
    {
        getconnectioncount,
        getpeerinfo,
        ping,
        addnode,
        getaddednodeinfo,
        getnettotals,

        dumpprivkey,
        importprivkey,
        importaddress,
        dumpwallet,
        importwallet,

        getgenerate,
        setgenerate,
        generate,
        generatetoaddress,

        getnetworkhashps,
        gethashespersec,
        getmininginfo,
        prioritisetransaction,
        getwork,
        getblocktemplate,
        submitblock,
        estimatefee,
        estimatesmartfee,

        getnewaddress,
        getaccountaddress,
        getrawchangeaddress,
        setaccount,
        getaccount,
        getaddressesbyaccount,
        sendtoaddress,
        signmessage,
        verifymessage,
        getreceivedbyaddress,
        getreceivedbyaccount,
        getaddressinfo,
        getbalance,
        getbalances,
        getunconfirmedbalance,
        movecmd,
        sendfrom,
        sendmany,
        addmultisigaddress,
        createmultisig,
        listreceivedbyaddress,
        listreceivedbyaccount,
        listtransactions,
        listaddressgroupings,
        listsinceblock,
        gettransaction,
        backupwallet,
        keypoolrefill,
        walletpassphrase,
        walletpassphrasechange,
        walletlock,
        encryptwallet,
        validateaddress,
        [Obsolete("Deprecated in Bitcoin Core 0.16.0 use getblockchaininfo, getnetworkinfo, getwalletinfo or getmininginfo instead")]
        getinfo,
        getwalletinfo,
        getblockchaininfo,
        getnetworkinfo,

        getrawtransaction,
        listunspent,
        lockunspent,
        listlockunspent,
        createrawtransaction,
        decoderawtransaction,
        decodescript,
        signrawtransaction,
        sendrawtransaction,
        gettxoutproof,
        verifytxoutproof,

        decodepsbt,
        combinepsbt,
        finalizepsbt,
        createpsbt,
        convertopsbt,
        walletprocesspsbt,
        walletcreatefundedpsbt,

        getblockcount,
        getblockfilter,
        getblockstats,
        getbestblockhash,
        getdifficulty,
        settxfee,
        getmempoolinfo,
        getrawmempool,
        testmempoolaccept,
        getblockhash,
        getblock,
        gettxoutsetinfo,
        gettxout,
        verifychain,
        getchaintips,
        invalidateblock,
        bumpfee,
        abandontransaction,
        signrawtransactionwithkey,
        scantxoutset,
        getmempoolentry,
        stop,
        uptime,
        createwallet,
        loadwallet,
        unloadwallet,
        addpeeraddress,
        savemempool,

        listwallets,
        listwalletdir,

    }

    public class RPCRequest
    {
        public RPCRequest(RPCOperations method, object[] parameters)
            : this(method.ToString(), parameters)
        {

        }

        public RPCRequest(string method, object[] parameters)
            : this()
        {
            Method = method;
            Params = parameters;
        }

        public RPCRequest()
        {
            JsonRpc = "1.0";
            Id = 1;
        }

        public string JsonRpc
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public string Method
        {
            get;
            set;
        }

        public object[] Params
        {
            get;
            set;
        }

        public bool ThrowIfRPCError { get; set; } = true;

        public Dictionary<string, object> NamedParams
        {
            get; set;
        }

        public void WriteJSON(TextWriter writer, bool withRPCVersionInfo = true)
        {
            var jsonWriter = new JsonTextWriter(writer);

            if (withRPCVersionInfo == true)
                WriteJSON(jsonWriter);

            else
                WriteJSONData(jsonWriter);

            jsonWriter.Flush();
        }

        internal void WriteJSON(JsonTextWriter writer)
        {
            writer.WriteStartObject();

            WriteProperty(writer, "jsonrpc", JsonRpc);
            WriteProperty(writer, "id", Id);
            WriteProperty(writer, "method", Method);

            writer.WritePropertyName("params");

            if (Params != null)
            {
                writer.WriteStartArray();
                for (int i = 0; i < Params.Length; i++)
                {
                    WriteValue(writer, Params[i]);
                }
                writer.WriteEndArray();
            }

            else if (NamedParams != null)
            {
                writer.WriteStartObject();
                foreach (var namedParam in NamedParams)
                {
                    writer.WritePropertyName(namedParam.Key);
                    WriteValue(writer, namedParam.Value);
                }
                writer.WriteEndObject();
            }

            else
            {
                writer.WriteStartArray();
                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }

        internal void WriteJSONData(JsonTextWriter writer)
        {
            writer.WriteStartObject();

            //if (withRPCVersionInfo == true)
            //{
            //    WriteProperty(writer, "jsonrpc", JsonRpc);
            //    WriteProperty(writer, "id", Id);
            //    WriteProperty(writer, "method", Method);
            //}

            //if (withparasPropertyName == true)
            //    writer.WritePropertyName("params");

            if (Params != null)
            {
                writer.WriteStartArray();
                for (int i = 0; i < Params.Length; i++)
                {
                    WriteValue(writer, Params[i]);
                }
                writer.WriteEndArray();
            }

            else if (NamedParams != null)
            {
                writer.WriteStartObject();
                foreach (var namedParam in NamedParams)
                {
                    writer.WritePropertyName(namedParam.Key);
                    WriteValue(writer, namedParam.Value);
                }
                writer.WriteEndObject();
            }

            else
            {
                writer.WriteStartArray();
                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }


        private void WriteValue(JsonTextWriter writer, object obj)
        {
            if (obj is JToken)
            {
                ((JToken)obj).WriteTo(writer);
            }
            else if (obj is Array)
            {
                writer.WriteStartArray();
                foreach (var x in (Array)obj)
                {
                    writer.WriteValue(x);
                }
                writer.WriteEndArray();
            }
            //else if (obj is uint256)
            //{
            //	writer.WriteValue(obj.ToString());
            //}
            else
            {
                writer.WriteValue(obj);
            }
        }

        private void WriteProperty<TValue>(JsonTextWriter writer, string property, TValue value)
        {
            writer.WritePropertyName(property);
            writer.WriteValue(value);
        }
    }

    public class RPCRequest2
    {
        readonly JObject joe = new JObject();
        public RPCRequest2(string methodName, List<object> parameters)
        {
            joe.Add(new JProperty("jsonrpc", "1.0"));
            joe.Add(new JProperty("id", "1"));
            joe.Add(new JProperty("method", methodName));

            JArray props = new JArray();

            foreach (var parameter in parameters)
            {
                props.Add(parameter);
            }

            var prosString = props.ToString();
            var prosString2 = JsonConvert.SerializeObject(props);

            joe.Add(new JProperty("params", prosString2));
        }

        public string ToString()
        {
            // serialize JSON for request
            return JsonConvert.SerializeObject(joe);
        }
    }

}
