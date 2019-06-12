using AutoPuTTY.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core.Launchers
{
    internal sealed class RdpConnectionLauncher : IConnectionLauncher
    {
        private static readonly string[] f = { "\\", "/", ":", "*", "?", "\"", "<", ">", "|" };
        private static readonly string[] ps = { "/", "\\\\" };
        private static readonly string[] pr = { "\\", "\\" };

        private readonly ConnectionDescription _connection;

        public RdpConnectionLauncher(ConnectionDescription connection)
        {
            _connection = connection;
        }

        public void Run()
        {
            string rdpPath = Environment.ExpandEnvironmentVariables(_connection.GetEffectiveAppPath());

            if (File.Exists(rdpPath))
            {
                var parameters = _connection.Parameters.Where(p => p.DisplayName != "Размер экрана").ToList();
                var size = _connection.Parameters.FirstOrDefault(p => p.DisplayName == "Размер экрана").Value;
                string[] sizes = size.Split('x');

                var outputFile = OtherHelper.ReplaceU(f, _connection.Name) + ".rdp";
                var outputFolderParam = _connection.Parameters.FirstOrDefault(p => p.Name == "OutputFolder");
                if (outputFolderParam != null && !string.IsNullOrEmpty(outputFolderParam.Value))
                {
                    outputFile = Path.Combine(outputFolderParam.Value, outputFile);
                }

                Directory.CreateDirectory(Path.GetDirectoryName(outputFile));

                TextWriter rdpFileWriter = new StreamWriter(path: outputFile);

                foreach (var p in parameters)
                {
                    rdpFileWriter.WriteLine(p.GetCommandLine());
                }

                rdpFileWriter.WriteLine(sizes.Length == 2 ? "desktopwidth:i:" + sizes[0] : "");
                rdpFileWriter.WriteLine(sizes.Length == 2 ? "desktopheight:i:" + sizes[1] : "");

                rdpFileWriter.Close();

                Process myProc = new Process
                {
                    StartInfo =
                    {
                        FileName = rdpPath,
                        Arguments = $"\"{outputFile}\"",
                    }
                };

                myProc.Start();
            }
        }
    }
}
