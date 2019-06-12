using JsonSubTypes;
using Newtonsoft.Json;
using System;
namespace AutoPuTTY.Core.Parameters
{
    [JsonConverter(typeof(JsonSubtypes), "ParamType")]
    public abstract class ConnectionParameterBase
    {
        public ConnectionParameterBase()
        {
            ParamType = GetType().Name;
        }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Value{ get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool IsAdditional { get; set; }

        public abstract string GetCommandLine();

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int CmdLineOrder { get; set; }

        public string ParamType { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool IsHidden { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string NextParameterDelimiter { get; set; }

        public bool TrySetValue(string value, out ErrorInfo errorInfo) {
            if (CanSetValue(value, out errorInfo)) {
                this.Value = value;
                return true; 
            }

            return false;
        } 

        protected virtual bool CanSetValue(string value, out ErrorInfo errorInfo) {
            errorInfo = null;
            return true; 
        }
    }
}