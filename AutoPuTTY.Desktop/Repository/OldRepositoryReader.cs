using AutoPuTTY.Core;
using AutoPuTTY.Core.Parameters;
using AutoPuTTY.Desktop;
using AutoPuTTY.Utils;
using AutoPuTTY.Utils.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Repository
{
    public class OldRepositoryReader
    {

        /*
  <Config ID="puttycommand">asdas</Config>
  <Config ID="puttykey">False</Config>
  <Config ID="puttyexecute">True</Config>
  <Config ID="rddrives">True</Config>
  <Config ID="00000000-0000-0000-0000-000000000000_rdsize">1280x1024</Config>
  <Config ID="ammyadminpath">C:\Windows\System32\calc.exe</Config>
  <Config ID="farnetboxpath">calc.exe</Config>
  <Config ID="teamviewerpath">C:\Windows\System32\DmOmaCpMo.exe</Config>
  <Config ID="radminpath">AxInstUI.exe</Config>
  <Config ID="plinkpath">plink.exe</Config>
  <Config ID="ncpath">nc64.exe</Config>
  <Config ID="winscppassive">True</Config>
  <Config ID="winscp">winscp.exe</Config>
  <Config ID="vnc">C:\Windows\System32\bcdboot.exe</Config>
  <Config ID="rdsize">
  </Config>
  <Config ID="remotedesktop">C:\Windows\System32\mstsc.exe</Config>
  <Config ID="putty">C:\Windows\System32\calc.exe</Config>          
         */
        private readonly Dictionary<string, string> _appPathKeyMapping = new Dictionary<string, string>()
        {
            { KnownConnections.Rdp().ConnectionTypeName, "rdpath"},
            { KnownConnections.RAdmin().ConnectionTypeName, "radminpath" },
            { KnownConnections.Ammy().ConnectionTypeName, "ammyadminpath"},
            { KnownConnections.PLink().ConnectionTypeName, "plinkpath" },
            { KnownConnections.Ssh().ConnectionTypeName, "putty"},
            { KnownConnections.Vnc().ConnectionTypeName, "vnc"},
            { KnownConnections.Telnet().ConnectionTypeName, "putty"},
            { KnownConnections.WinScpScp().ConnectionTypeName, "winscp"},
            { KnownConnections.WinScpFtp().ConnectionTypeName, "winscp"},
            { KnownConnections.WinScpSftp().ConnectionTypeName, "winscp"},
        };
 
        private readonly XmlHelper _xmlHelper;

        public string ConfigFile { get; }

        private readonly KnownConnections _knownConnections;

        public OldRepositoryReader(string configFile, KnownConnections knownConnections)
        {
            _xmlHelper = new XmlHelper();
            ConfigFile = configFile;
            _knownConnections = knownConnections;
        }

        public List<ConnectionGroup> Load()
        {
            var oldConfigFile = "autoputty.xml";

            if (File.Exists(oldConfigFile))
            {
                PatchProfiles(oldConfigFile);

                var oldRepository = _xmlHelper.getAllData(oldConfigFile);
                var groups = new List<ConnectionGroup>();
                foreach (var item in oldRepository)
                {
                    if (item is GroupElement)
                    {
                        var group = ReadGroup((GroupElement)item);
                        groups.Add(group);
                    }
                }
                try
                {
                    File.Move("autoputty.xml", "autoputty.xml.bak");
                }
                catch (Exception)
                {
                }
                return groups;
            }

            return null;
        }

        private void PatchProfiles(string oldConfigFile)
        {
            foreach (var pair in _appPathKeyMapping)
            {
                ConnectionDescription profile;
                if (_knownConnections._knownConnectionProfiles.TryGetValue(pair.Key, out profile))
                {
                    string pathMapping;
                    if (_appPathKeyMapping.TryGetValue(profile.ConnectionTypeName, out pathMapping))
                    {
                        var oldRepoValue = _xmlHelper.configGet(oldConfigFile, pathMapping);
                        if (!string.IsNullOrEmpty(oldRepoValue))
                        {
                            profile.AppPath = oldRepoValue;
                        }
                    }

                    if (pair.Key == KnownConnections.Ssh().ConnectionTypeName)
                    {
                        PatchSshProfile(oldConfigFile, profile);
                    }
                    else if (pair.Key == KnownConnections.Rdp().ConnectionTypeName)
                    {
                        PatchRdpProfile(oldConfigFile, profile);
                    }
                    else if (pair.Key == KnownConnections.WinScpFtp().ConnectionTypeName
                        || pair.Key == KnownConnections.WinScpScp().ConnectionTypeName
                        || pair.Key == KnownConnections.WinScpSftp().ConnectionTypeName)
                    {
                        PatchWinScpProfile(oldConfigFile, profile);
                    }
                    else if (pair.Key == KnownConnections.Vnc().ConnectionTypeName)
                    {
                        PatchVncProfile(oldConfigFile, profile);
                    }
                }
            }
        }

        private void PatchVncProfile(string oldConfigFile, ConnectionDescription profile)
        {
            PatchBoolParameter(oldConfigFile, profile, "IsFullScreen", "vncfullscreen");
            PatchBoolParameter(oldConfigFile, profile, "ViewOnly", "vncviewonly");
        }

        private void PatchSshProfile(string oldConfigPath, ConnectionDescription sshProfile)
        {
            PatchBoolParameter(oldConfigPath, sshProfile, "Forwarding", "puttyforward");
            PatchFileParameter(oldConfigPath, sshProfile, "CommandFile", "puttycommand");
            PatchFileParameter(oldConfigPath, sshProfile, "KeyFile", "puttykeyfile");
        }

        private void PatchRdpProfile(string oldConfigPath, ConnectionDescription rdpProfile)
        {
            PatchBoolParameter(oldConfigPath, rdpProfile, "Drives", "rddrives");
            PatchBoolParameter(oldConfigPath, rdpProfile, "Admin", "rdadmin");
            PatchBoolParameter(oldConfigPath, rdpProfile, "Multimonitor", "rdspan");
            PatchPredefinedParameter(oldConfigPath, rdpProfile, "ScreenSize", "rdsize");
            PatchFolderParameter(oldConfigPath, rdpProfile, "OutputFolder", "rdfilespath");
        }

        private void PatchWinScpProfile(string oldConfigPath, ConnectionDescription winScp)
        {
            PatchFileParameter(oldConfigPath, winScp, "KeyFile", "winscpkeyfile");
            PatchBoolParameter(oldConfigPath, winScp, "Passive", "winscppassive");
        }

        private void PatchPredefinedParameter(string oldConfigPath, ConnectionDescription profile, string newParamName, string oldParamName)
        {
            var parameters = profile.Parameters;
            if (parameters.FirstOrDefault(p => p.Name == newParamName) is PredefinedConnectionParameter predefinedParam)
            {
                var oldValue = _xmlHelper.configGet(oldConfigPath, oldParamName);
                if (!string.IsNullOrEmpty(oldValue))
                {
                    predefinedParam.Value = oldValue;
                }
            }
        }

        private void PatchBoolParameter(string oldConfigPath, ConnectionDescription profile, string newParamName, string oldParamName)
        {
            var parameters = profile.Parameters;
            if (parameters.FirstOrDefault(p => p.Name == newParamName) is SwitchConnectionParameter switchParam)
            {
                if (bool.TryParse(_xmlHelper.configGet(oldConfigPath, oldParamName), out bool f))
                {
                    switchParam.Value = f ? switchParam.OnValue : switchParam.OffValue;
                    switchParam.IsOn = f;
                }
            }
        }

        private void PatchFileParameter(string oldConfigPath, ConnectionDescription profile, string newParamName, string oldParamName) 
        {
            if (profile.Parameters.FirstOrDefault(p => p.Name == newParamName) is FilePathConnectionParameter fileParam)
            {
                var oldValue = _xmlHelper.configGet(oldConfigPath, oldParamName);
                if (!string.IsNullOrEmpty(oldValue))
                {
                    fileParam.Value = oldValue;
                }
            }
        }

        private void PatchFolderParameter(string oldConfigPath, ConnectionDescription profile, string newParamName, string oldParamName)
        {
            if (profile.Parameters.FirstOrDefault(p => p.Name == newParamName) is FolderPathConnectionParameter folderParam)
            {
                var oldValue = _xmlHelper.configGet(oldConfigPath, oldParamName);
                if (!string.IsNullOrEmpty(oldValue))
                {
                    folderParam.Value = oldValue;
                }
            }
        }

        private void PatchStringParameter(string oldConfigPath, ConnectionDescription profile, string newParamName, string oldParamName)
        {
            if (profile.Parameters.FirstOrDefault(p => p.Name == newParamName) is StringConnectionParameter stringParam)
            {
                var oldValue = _xmlHelper.configGet(oldConfigPath, oldParamName);
                if (!string.IsNullOrEmpty(oldValue))
                {
                    stringParam.Value = oldValue;
                }
            }
        }

        private ConnectionGroup ReadGroup(GroupElement groupElement)
        {
            var connectionGroup = new ConnectionGroup
            {
                Name = groupElement.groupName, 
            };

            PatchCommonParameters(connectionGroup.Parameters,
                () => groupElement.defaultHost, () => groupElement.defaultPort, 
                () => groupElement.defaultUsername, () => groupElement.defaultPassword);

            foreach (var item in groupElement.servers)
            {
                var connection = ReadConnection((ServerElement)item);
                if (connection != null)
                {
                    connectionGroup.Connections.Add(connection);
                }
            }
            return connectionGroup;
        }

        private ConnectionDescription ReadConnection(ServerElement server)
        {
            var connection = _knownConnections.CreateFromProfile(server.Type.ConvertToString());
            if (connection != null)
            {
                connection.Name = server.Name;
                PatchCommonParameters(connection.Parameters, 
                    () => server.Host, () => server.Port, () => server.Name, () => server.Password);
            }
            return connection;
        }

        private void PatchCommonParameters(IEnumerable<ConnectionParameterBase> parameters, 
            Func<string> hostGetter, Func<string> portGetter, Func<string> userGetter, Func<string> passGetter)
        {
            var host = parameters.FirstOrDefault(p => p.Name == KnownParameters.Host);
            var port = parameters.FirstOrDefault(p => p.Name == KnownParameters.Port);
            var user = parameters.FirstOrDefault(p => p.Name == KnownParameters.User);
            var pass = parameters.FirstOrDefault(p => p.Name == KnownParameters.Password);

            if (host != null)
            {
                host.Value = hostGetter();
            }

            if (port != null)
            {
                port.Value = portGetter();
            }

            if (user != null)
            {
                user.Value = userGetter();
            }

            if (pass != null)
            {
                pass.Value = passGetter();
            }
        }
    }
}
