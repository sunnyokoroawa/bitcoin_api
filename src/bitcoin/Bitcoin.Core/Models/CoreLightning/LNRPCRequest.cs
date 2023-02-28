using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bitcoin.Core.Models.CoreLightning
{
    public enum LNRPCOperations
    {
        getinfo,
        newaddr,
        listfunds,
        listpeers,
        invoice,
        listinvoices,
        connect,
        disconnect,
        fundchannel,
        decode,
        delinvoice,
        delexpiredinvoice,
        paystatus,
        offer,

        //undone
        listpays,
        decodepay,
        listsendpays,
        fetchinvoice,
        sendinvoice,

        listchannels,
        listnodes,

        //pay route
        pay, //searches for pay route and then makes payment. its shower than sendpay
        getroute,
        sendpay, //expects the payment route as a param
        withdraw

    }
    public class LNRPCRequest
    {
        public LNRPCRequest(LNRPCOperations method, object[] parameters)
            : this(method.ToString(), parameters)
        {

        }

        public LNRPCRequest(string method, object[] parameters)
            : this()
        {
            Method = method;
            Params = parameters;
        }

        public LNRPCRequest()
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
            //else if (obj is int)
            //{
            //    writer.WriteValue(obj);
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
