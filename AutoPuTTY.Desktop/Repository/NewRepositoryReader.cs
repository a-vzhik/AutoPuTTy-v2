using AutoPuTTY.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

using System;
using System.Linq;
using AutoPuTTY.Desktop;

namespace AutoPuTTY.Repository
{
    public class NewRepositoryReader
    {
        private readonly KnownConnections _knownConnections;

        public NewRepositoryReader(KnownConnections knownConnections)
        {
            _knownConnections = knownConnections;
        }

        public void LoadProfiles(string profilesFile)
        {
            if (File.Exists(profilesFile))
            {
                var text = File.ReadAllText(profilesFile);
                var profiles = JsonConvert.DeserializeObject<Dictionary<string, ConnectionDescription>>(text);

                foreach (var p in profiles)
                    _knownConnections.MergeProfile(p.Value);
            }
        }

        public List<ConnectionGroup> Load(string configFile)
        {
            if (!File.Exists(configFile))
            {
                return new List<ConnectionGroup>();
            }

            var json = File.ReadAllText(configFile);
            var groupInstances = JsonConvert.DeserializeObject<List<ConnectionGroupInstance>>(json);

            return groupInstances.Select(gi =>
                new ConnectionGroup(gi.Parameters)
                {
                    Name = gi.Name,
                    
                    Connections = gi.Connections.Select(ci =>
                    {
                        var connectionDescription = _knownConnections.CreateFromProfile(ci.ConnectionTypeName);
                        connectionDescription.Name = ci.ConnectionName;
                        connectionDescription.Description = ci.Description;
                        connectionDescription.IsAutoCheckEnabled = ci.IsAutoCheckEnabled;
                        var ciParamMap = ci.Parameters.ToDictionary(p => p.Name, p => p.Value);
                        foreach (var profileParam in connectionDescription.Parameters)
                        {
                            if (ciParamMap.ContainsKey(profileParam.Name))
                            {
                                profileParam.Value = ciParamMap[profileParam.Name];
                            }
                        };

                        return connectionDescription;
                    }).ToList()
                }).ToList();
        }

        [Obsolete("Hack for migrating from old config file to new config file. Normally profiles config should not be patched.")]
        public void SaveProfiles(string profilesFile, IDictionary<string, ConnectionDescription> profiles)
        {
            File.WriteAllText(profilesFile, JsonConvert.SerializeObject(profiles, Formatting.Indented));
        }

        public void Save(string connectionsFile, List<ConnectionGroup> groups)
        {
            var transformed = groups.Select(group =>
                new ConnectionGroupInstance()
                {
                    Name = group.Name,
                    Parameters = group.Parameters
                        .Select(gp => new ConnectionParameterIntance
                        {
                            Name = gp.Name, 
                            Value = gp.Value
                        })
                        .ToList(),
                    Connections = group.Connections
                        .Select(c => new ConnectionInstance
                        {
                            ConnectionName = c.Name,
                            Description = c.Description,
                            IsAutoCheckEnabled = c.IsAutoCheckEnabled,
                            ConnectionTypeName = c.ConnectionTypeName,
                            Parameters = c.Parameters
                                .Where(p => !string.IsNullOrEmpty(p.Value))
                                .Select(p => new ConnectionParameterIntance
                                {
                                    Name = p.Name,
                                    Value = p.Value
                                })
                                .ToList()
                        })
                        .ToList()
                }
            ).ToList();

            var json = JsonConvert.SerializeObject(transformed, Formatting.Indented);

            File.WriteAllText(connectionsFile, json);
        }
    }
}
