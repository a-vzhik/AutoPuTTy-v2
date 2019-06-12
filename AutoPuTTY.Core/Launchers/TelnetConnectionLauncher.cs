using System;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace AutoPuTTY.Core.Launchers
{
    internal class TelnetConnectionLauncher : IConnectionLauncher
    {
        private ConnectionDescription connection;

        public TelnetConnectionLauncher(ConnectionDescription connection)
        {
            this.connection = connection;
        }

        public void Run()
        {
            if (File.Exists(connection.GetEffectiveAppPath()))
            {
                string host = connection.Parameters.First(p => p.Name == KnownParameters.Host).Value;
                string port = connection.Parameters.First(p => p.Name == KnownParameters.Port).Value;

                using (Process puttyProcess = new Process())
                {
                    puttyProcess.StartInfo.FileName = connection.GetEffectiveAppPath();
                    puttyProcess.StartInfo.Arguments = " -telnet ";

                    string username = connection.Parameters.First(p => p.Name == KnownParameters.User).Value;
                    string password = connection.Parameters.First(p => p.Name == KnownParameters.Password).Value;

                    puttyProcess.StartInfo.Arguments += !string.IsNullOrEmpty(username) ? username + "@" : "";

                    puttyProcess.StartInfo.Arguments += !string.IsNullOrEmpty(host) ? host : "";
                    puttyProcess.StartInfo.Arguments += !string.IsNullOrEmpty(port) ? " " + port : "";

                    puttyProcess.Start();

                    Thread.Sleep(1500);

                    IntPtr h = puttyProcess.MainWindowHandle;

                    SetForegroundWindow(h);
                    SendKeys.SendWait(username);
                    SendKeys.SendWait("~");
                    SendKeys.SendWait(password);
                    SendKeys.SendWait("~");
                }
            }
        }

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

    }
}