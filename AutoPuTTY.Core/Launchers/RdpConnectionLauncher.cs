using AutoPuTTY.Core.Parameters;
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

                var outputFile = OtherHelper.ReplaceU(f, _connection.Name) + ".rdp";
                var outputFolderParam = _connection.Parameters.FirstOrDefault(p => p.Name == "OutputFolder");
                if (outputFolderParam != null && !string.IsNullOrEmpty(outputFolderParam.Value))
                {
                    Directory.CreateDirectory(outputFolderParam.Value);
                    outputFile = Path.Combine(outputFolderParam.Value, outputFile);
                }

                TextWriter rdpFileWriter = new StreamWriter(path: outputFile);

                foreach (var p in parameters.Where(p => !(p is FolderPathConnectionParameter)))
                {
                    var line = p.GetCommandLine();
                    if (string.IsNullOrEmpty(line)) {
                        continue;
                    }
                    rdpFileWriter.Write(line);
                    var delimiter = p.NextParameterDelimiter ?? _connection.ParameterDelimiter;
                    rdpFileWriter.Write(delimiter);
                }

                var fullScreen = _connection.Parameters.FirstOrDefault(p => p.Name == "IsFullScreen") as SwitchConnectionParameter;
                if (fullScreen != null && !fullScreen.IsOn)
                {
                    var size = _connection.Parameters.FirstOrDefault(p => p.Name == "ScreenSize").Value;
                    string[] sizes = size.Split('x');

                    rdpFileWriter.WriteLine(sizes.Length == 2 ? "desktopwidth:i:" + sizes[0] : "");
                    rdpFileWriter.WriteLine(sizes.Length == 2 ? "desktopheight:i:" + sizes[1] : "");
                }

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
