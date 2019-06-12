using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core.Parameters
{
    public class StringConnectionParameter : ConnectionParameterBase
    {
        public StringConnectionParameter()
        {
            Delimiter = " ";
            Transformation = s => s;
        }

        public string CmdLineName { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Delimiter { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool IsQuoted { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public Func<string, string> Transformation
        {
            get;
            set;
        }

        public override string GetCommandLine()
        {
            if (string.IsNullOrEmpty(Value))
                return string.Empty;

            var transformed = Transformation != null ? Transformation(Value) : Value;
            if (IsQuoted)
            {
                transformed = $"\"{transformed}\"";
            }

            return $"{CmdLineName}{Delimiter}{transformed}";
        }
    }
}
