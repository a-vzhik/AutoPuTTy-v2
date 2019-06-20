using AutoPuTTY.Core.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core
{
    public class ConnectionGroup
    {
        public ConnectionGroup(IList<ConnectionParameterIntance> parameterInstances = null)
        {
            Connections = new List<ConnectionDescription>();
            Groups = new List<ConnectionGroup>();

            Parameters = new ConnectionParameterBase[]
            {
                new StringConnectionParameter()
                {
                    Name = KnownParameters.Host,
                    DisplayName = "Хост по умолчанию",
                    Delimiter = "",
                    Value = string.Empty,
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Port,
                    DisplayName = "Порт по умолчанию",
                    Delimiter = "",
                    Value = string.Empty,
                },

                new StringConnectionParameter
                {
                    Name = "User",
                    DisplayName = "Юзер по умолчанию",
                    Delimiter = "",
                    Value = string.Empty
                },

                new StringConnectionParameter
                {
                    Name = "Password",
                    DisplayName = "Пароль по умолчанию",
                    Delimiter = "",
                    Value = string.Empty,
                },

            }.ToList();

            if (parameterInstances != null)
            {
                foreach (var param in Parameters)
                {
                    var pi = parameterInstances.FirstOrDefault(p => p.Name == param.Name);
                    if (pi != null && !string.IsNullOrEmpty(pi.Value))
                    {
                        param.Value = pi.Value;
                    }
                }
            }
        }

        public string Name { get; set; }

        public IList<ConnectionDescription> Connections { get; set; }

        public IList<ConnectionParameterBase> Parameters { get; set; }

        public IList<ConnectionGroup> Groups { get; set; }

        public bool ReplaceConnection(ConnectionDescription oldConnection, ConnectionDescription newConnection)
        {
            var oldIndex = Connections.IndexOf(oldConnection);
            if (oldIndex >= 0)
            {
                Connections.RemoveAt(oldIndex);
                Connections.Insert(oldIndex, newConnection);
                return true;
            }

            return false;
        }
    }
}
