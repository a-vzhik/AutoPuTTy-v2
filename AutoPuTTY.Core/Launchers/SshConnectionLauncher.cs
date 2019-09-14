using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core.Launchers
{
    internal class SshConnectionLauncher : IConnectionLauncher
    {
        private readonly ConnectionDescription _connection;
        private readonly Func<ConnectionDescription, ConnectionCmdLineGenerator> _cmdLineGeneratorFactory;

        public SshConnectionLauncher(
            ConnectionDescription connection,
            Func<ConnectionDescription, ConnectionCmdLineGenerator> generatorFactory)
        {
            _connection = connection;
            _cmdLineGeneratorFactory = generatorFactory;
        }

        public void Run()
        {
            var appPath = _connection.GetEffectiveAppPath();
            if (!File.Exists(appPath))
            {
                return;
            }

            var generator = _cmdLineGeneratorFactory(_connection);

            var args = generator.GetArgumentsLine();
            Process.Start(appPath, args);
        }
    }
}
