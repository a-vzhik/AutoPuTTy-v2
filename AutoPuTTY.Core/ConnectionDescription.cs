using AutoPuTTY.Core.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core
{
    public class ConnectionDescription
    {
        private readonly List<ConnectionDescription> _instances = new List<ConnectionDescription>();
        private ConnectionDescription _profile;

        public ConnectionDescription()
        {
            ParameterDelimiter = " ";
        }

        [JsonIgnore]
        public IEnumerable<ConnectionDescription> Instances
        {
            get
            {
                return _instances;
            }
        }

        public string GetEffectiveAppPath()
        {
            return (_profile ?? this).AppPath;
        }

        [JsonIgnore]
        public ConnectionDescription Profile {
            get
            {
                return _profile;
            }
            set
            {
                if (_profile != value)
                {
                    _profile = value;
                    if (_profile != null)
                        _profile._instances.Add(this);
                    else
                        _profile?._instances.Remove(this);
                }
            }
        }

        public string Name { get; set; }

        public string ConnectionTypeName { get; set; }

        public string AppPath { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsPaused { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string ParameterDelimiter { get; set; }

        public IEnumerable<ConnectionParameterBase> Parameters { get; set; }

        [JsonIgnore]
        public Uri IconUri { get; set; }
    }
}
