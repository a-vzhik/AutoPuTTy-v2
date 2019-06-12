using AutoPuTTY.Core.Launchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core
{
    public sealed class ConnectionLauncherProvider 
    {
        public IConnectionLauncher GetLauncher(ConnectionDescription connection)
        {
            if (connection.ConnectionTypeName == "Remote Desktop")
            {
                return new RdpConnectionLauncher(connection);
            }

            if (connection.ConnectionTypeName == "Vnc")
            {
                return new VncConnectionLauncher(connection);
            }

            if (connection.ConnectionTypeName == "Telnet")
            {
                return new TelnetConnectionLauncher(connection);
            }

            return new DefaultConnectionLauncher(connection, c => new ConnectionCmdLineGenerator(c));
        }
    }
}
