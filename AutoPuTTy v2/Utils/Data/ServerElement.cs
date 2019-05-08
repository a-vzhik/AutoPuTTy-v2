﻿using System;

namespace AutoPuTTY.Utils.Data
{
    public class ServerElement
    {
        public Guid Id;
        public string Name;

        public string Host;
        public string Port;
        public string Username;
        public string Password;

        public ConnectionType Type;
        public bool AutoChecks;

        public string HostWithPort;

        public ServerElement(string name, string host, string port, 
            string username, string password, string type, bool autoChecks)
        {
            Name = name.Trim();

            Host = host.Trim();
            Port = port.Trim();
            Username = username.Trim();
            Password = password.Trim();

            Type = (ConnectionType) Int32.Parse(type.Trim());
            AutoChecks = autoChecks;

            HostWithPort = string.IsNullOrEmpty(Port) ? Host : string.Concat(Host, ":", Port);
        }

    }
}
