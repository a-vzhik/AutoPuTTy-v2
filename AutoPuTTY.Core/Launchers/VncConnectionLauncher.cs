using AutoPuTTY.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core.Launchers
{
    public class VncConnectionLauncher : IConnectionLauncher
    {
        private static readonly string[] f = { "\\", "/", ":", "*", "?", "\"", "<", ">", "|" };

        private readonly ConnectionDescription _connection;

        public VncConnectionLauncher(ConnectionDescription connection)
        {
            _connection = connection;
        }


        public void Run()
        {
            if (File.Exists(_connection.GetEffectiveAppPath()))
            {
                var hostParam = _connection.Parameters.First(p => p.Name == KnownParameters.Host);
                var portParam = _connection.Parameters.First(p => p.Name == KnownParameters.Port);


                var vncFilePath = OtherHelper.ReplaceU(f, _connection.Name) + ".vnc";
                var vncOutPathParam = _connection.Parameters.First(p => p.Name == "OutputFolder");
                if (vncOutPathParam != null && Directory.Exists(vncOutPathParam.Value))
                {
                    vncFilePath = Path.Combine(vncOutPathParam.Value, vncFilePath);
                }
                TextWriter vncFile = new StreamWriter(vncFilePath);

                vncFile.WriteLine("[Connection]");
                vncFile.WriteLine(hostParam.GetCommandLine());
                vncFile.WriteLine(portParam.GetCommandLine());

                var userParam = _connection.Parameters.First(p => p.Name == KnownParameters.User);
                vncFile.WriteLine(userParam.GetCommandLine());
                var passParam = _connection.Parameters.First(p => p.Name == KnownParameters.Password);
                vncFile.WriteLine(passParam.GetCommandLine());

                vncFile.WriteLine("[Options]");
                var fullScreenParam = _connection.Parameters.First(p => p.Name == "IsFullScreen");
                vncFile.WriteLine(fullScreenParam.GetCommandLine());

                var viewOnly = _connection.Parameters.First(p => p.Name == "ViewOnly");
                vncFile.WriteLine(viewOnly.GetCommandLine());

                vncFile.WriteLine(passParam.Value != "" && passParam.Value.Length > 8 ? "protocol3.3=1" : ""); // fuckin vnc 4.0 auth

                vncFile.Close();

                Process myProc = new Process
                {
                    StartInfo =
                    {
                        FileName = _connection.GetEffectiveAppPath(),
                        Arguments = "-config \"" + vncFilePath
                    }
                };

                myProc.Start();
            }
        }
    }
}
