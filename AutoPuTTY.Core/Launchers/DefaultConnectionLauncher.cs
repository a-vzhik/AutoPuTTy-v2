using System;
using System.Diagnostics;
using System.IO;

namespace AutoPuTTY.Core.Launchers
{
    public class DefaultConnectionLauncher : IConnectionLauncher
    {
        private readonly ConnectionDescription _connection;
        private readonly Func<ConnectionDescription, ConnectionCmdLineGenerator> _cmdLineGeneratorFactory;

        public DefaultConnectionLauncher(
            ConnectionDescription connection, 
            Func<ConnectionDescription, ConnectionCmdLineGenerator> generatorFactory)
        {
            _connection = connection;
            _cmdLineGeneratorFactory = generatorFactory;
        }

        public void Run() {
            if (!File.Exists(_connection.GetEffectiveAppPath()))
            {
                return;
            }

            var generator = _cmdLineGeneratorFactory(_connection);

            var args = string.Format("/C {0}", generator.GetCommandLine());
            Process.Start("CMD.exe", args);
        }
    }
}
