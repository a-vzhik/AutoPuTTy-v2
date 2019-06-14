using AutoPuTTY.Core.Data;
using Newtonsoft.Json;

namespace AutoPuTTY.Core
{
    public class ConnectionParameterIntance
    {
        public string Name { get; set; }

        [JsonConverter(typeof(EncryptingJsonConverter))]
        public string Value { get; set; }
    }
}