using AutoPuTTY.Core;
using AutoPuTTY.Core.Parameters;
using AutoPuTTY.Repository;
using AutoPuTTY.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoPuTTY.Desktop
{
    public class KnownConnections
    {
        public readonly Dictionary<string, ConnectionDescription> _knownConnectionProfiles = new Dictionary<string, ConnectionDescription>();

        public KnownConnections()
        {
            AddConnection(Ssh());
            AddConnection(Rdp());
            AddConnection(PLink());
            AddConnection(Telnet());
            AddConnection(Vnc());
            AddConnection(WinScpScp());
            AddConnection(WinScpFtp());
            AddConnection(WinScpSftp());
            AddConnection(RAdmin());
            AddConnection(TeamViewer());
            AddConnection(Ammy());
        }

        public ConnectionDescription CreateFromProfile(string name)
        {
            if (_knownConnectionProfiles.TryGetValue(name, out ConnectionDescription profile))
            {
                var json = JsonConvert.SerializeObject(profile);
                var description = JsonConvert.DeserializeObject<ConnectionDescription>(json);
                description.Profile = profile;

                if (profile.ConnectionTypeName == "Telnet")
                {
                    ConnectionDescription sshProfile;
                    if (_knownConnectionProfiles.TryGetValue("SSH", out sshProfile))
                    {
                        profile.AppPath = sshProfile.AppPath;
                        description.AppPath = sshProfile.AppPath;
                    }
                }

                return description;
            }

            return null;
        }

        private void AddConnection(ConnectionDescription cd)
        {
            _knownConnectionProfiles.Add(cd.ConnectionTypeName, cd);
        }


        public static ConnectionDescription Rdp()
        {
            var connection = new ConnectionDescription();
            connection.AppPath = "C:\\\\Windows\\System32\\mstsc.exe";
            connection.ConnectionTypeName = "Remote Desktop";
            connection.Name = "My Rdp";
            connection.ParameterDelimiter = "\n";
            connection.Parameters = new ConnectionParameterBase[]
            {
                //new StringConnectionParameter()
                //{
                //    Name = "HostAndPort",
                //    DisplayName = "Хост и порт",
                //    CmdLineName = "full address:s:",
                //    Purpose = KnownPurpose.HostAndPort,
                //    Delimiter = "",
                //    Value = "localhost:5555", 

                //},

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Host,
                    DisplayName = "Хост",
                    CmdLineName = "full address:s:",
                    Delimiter = "",
                    Value = "localhost", 
                    NextParameterDelimiter = ":"
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Port,
                    DisplayName = "Порт",
                    CmdLineName = "",
                    Delimiter = "",
                    Value = "5555",

                },

                new StringConnectionParameter
                {
                    Name = "User",
                    DisplayName = "[Домен\\]Пользователь",
                    CmdLineName = "username:s:",
                    Delimiter = "",
                    Value = string.Empty
                },

                new RdpPasswordConnectionParameter
                {
                    Name = "Password",
                    DisplayName = "Пароль",
                    CmdLineName = "password 51:b:",
                    Delimiter = "",
                    Value = string.Empty,
                },

                new SwitchConnectionParameter
                {
                    Name = "IsFullScreen",
                    DisplayName = "Полный экран",
                    OnValue = "screen mode id:i:2",
                    OffValue = "screen mode id:i:1",
                    Value = "screen mode id:i:2",
                    IsOn = true
                },

                new PredefinedConnectionParameter
                {
                    Name = "ScreenSize",
                    DisplayName = "Размер экрана",
                    CmdLineName = "",
                    Delimiter = "",
                    AllowedValues = new []
                    {
                        "640x480",
                        "800x600",
                        "1024x768", 
                        "1152x864", 
                        "1280x720",
                        "1280x960",
                        "1280x1024",
                        "1600x1200",
                        "1680x1050",
                        "1920x1080",
                        "1920x1200",
                        "Full screen"
                    }.ToList(),
                    Value = string.Empty
                }, 

                new SwitchConnectionParameter
                {
                    Name = "Drives",
                    DisplayName = "Диски", 
                    OnValue = "redirectdrives:i:1", 
                    OffValue = string.Empty
                }, 

                new SwitchConnectionParameter
                {
                    Name = "Admin",
                    DisplayName = "Админ-сессия", 
                    OnValue =  "administrative session:i:1", 
                    OffValue = string.Empty
                }, 

                new SwitchConnectionParameter
                {
                    Name = "Multimonitor",
                    DisplayName = "Мультимонитор", 
                    OnValue = "use multimon:i:1", 
                    OffValue = string.Empty,
                }, 

                new FolderPathConnectionParameter
                {
                    Name = "OutputFolder", 
                    DisplayName = "Папка с RDP-файлами",
                    Value = string.Empty
                }
               
            };
            return connection;
        }

        internal static Uri GetIconUrl(string connectionTypeName)
        {
            switch (connectionTypeName)
            {
                case "SSH":
                case "Telnet":
                case "PLink":
                    return new Uri("pack://application:,,,/Images/PuTTY_icon_128px.png");
                case "WinScp (FTP)":
                case "WinScp (SFTP)":
                case "WinScp (SCP)":
                    return new Uri("pack://application:,,,/Images/WinSCP_Logo.png");
                case "RAdmin":
                    return new Uri("pack://application:,,,/Images/radmin.png");
                case "Vnc":
                    return new Uri("pack://application:,,,/Images/vnc.png");
                case "Ammy Admin":
                    return new Uri("pack://application:,,,/Images/ammy.png");
                case "Remote Desktop":
                    return new Uri("pack://application:,,,/Images/rdp.png");
                default:
                    return null;
            }
        }

        public static ConnectionDescription Vnc()
        {
            var connection = new ConnectionDescription();
            connection.AppPath = "vnc.exe";
            connection.ConnectionTypeName = "Vnc";
            connection.Name = "My Vnc";
            connection.ParameterDelimiter = "\n";
            connection.Parameters = new ConnectionParameterBase[]
            {
                new StringConnectionParameter()
                {
                    Name = KnownParameters.Host,
                    DisplayName = "Хост",
                    CmdLineName = "host",
                    Delimiter = "=",
                    Value = "localhost",
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Port,
                    DisplayName = "Порт",
                    CmdLineName = "port",
                    Delimiter = "=",
                    Value = "5900",

                },

                new StringConnectionParameter
                {
                    Name = "User",
                    DisplayName = "Пользователь",
                    CmdLineName = "username",
                    Delimiter = "=",
                    Value = string.Empty
                },

                new VncPasswordConnectionParameter
                {
                    Name = "Password",
                    DisplayName = "Пароль",
                    CmdLineName = "password",
                    Delimiter = "=",
                    Value = string.Empty,
                },

                new SwitchConnectionParameter
                {
                    Name = "IsFullScreen",
                    DisplayName = "Полный экран",
                    OnValue = "fullscreen=1",
                    OffValue = "",
                    Value = "",
                    IsOn = false
                },

                new SwitchConnectionParameter
                {
                    Name = "ViewOnly",
                    DisplayName = "Только просмотр",
                    OnValue = "viewonly=1\nsendptrevents=0\nsendkeyevents=0\nsendcuttext=0\nacceptcuttext=0\nsharefiles=0",
                    OffValue = string.Empty
                },

                new FolderPathConnectionParameter
                {
                    Name = "OutputFolder",
                    DisplayName = "Папка с VNC-файлами",
                    Value = string.Empty
                }

            };
            return connection;
        }

        public static ConnectionDescription RAdmin()
        {
            var radminConnection = new ConnectionDescription();
            radminConnection.AppPath = "msc-radmin.exe";
            radminConnection.ConnectionTypeName = "RAdmin";
            radminConnection.Name = "My RAdmin";
            radminConnection.Parameters = new ConnectionParameterBase[]
            {
                new StringConnectionParameter()
                {
                    Name = KnownParameters.Host,
                    DisplayName = "Хост",
                    CmdLineName = "/connect",
                    Delimiter = ":",
                    Value = "localhost", 
                    NextParameterDelimiter = ":"
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Port,
                    DisplayName = "Порт",
                    CmdLineName = "",
                    Delimiter = "",
                    Value = "5555"
                },

                new StringConnectionParameter
                {
                    Name = KnownParameters.User,
                    DisplayName = "Пользователь",
                    CmdLineName = "/user",
                    Delimiter = ":",
                    Value = string.Empty
                },

                new StringConnectionParameter
                {
                    Name = KnownParameters.Password,
                    DisplayName = "Пароль",
                    CmdLineName = "/pwd",
                    Delimiter = ":",
                    Value = string.Empty
                },

                new SwitchConnectionParameter
                {
                    Name = "IsFullScreen",
                    DisplayName = "Полный экран",
                    OnValue = "/fullscreen",
                    OffValue = string.Empty,
                    Value = string.Empty
                }
            };

            return radminConnection;
        }

        public static ConnectionDescription PLink()
        {
            var plinkConnection = new ConnectionDescription();
            plinkConnection.AppPath = "plink.exe";
            plinkConnection.ConnectionTypeName = "PLink";
            plinkConnection.Name = "My PLink";
            plinkConnection.IsPaused = true;

            plinkConnection.Parameters = new ConnectionParameterBase[]
            {
                new StringConnectionParameter()
                {
                    Name = KnownParameters.Host,
                    DisplayName = "Хост",
                    CmdLineName = "",
                    Delimiter = "",
                    Value = "localhost", 
                    CmdLineOrder = 1,
                },

                new SwitchConnectionParameter
                {
                    Name = "Ssh", 
                    IsOn = true, 
                    OnValue = "-ssh", 
                    OffValue = string.Empty
                },

                new StringConnectionParameter
                {
                    Name = KnownParameters.User,
                    DisplayName = "Пользователь",
                    CmdLineName = "-l",
                    Delimiter = " ",
                    Value = string.Empty
                },

                new StringConnectionParameter
                {
                    Name = KnownParameters.Password,
                    DisplayName = "Пароль",
                    CmdLineName = "-pw",
                    Delimiter = " ",
                    Value = string.Empty
                },

                new StringConnectionParameter
                {
                    Name = "Command",
                    DisplayName = "Команда",
                    CmdLineName = "",
                    Delimiter = "",
                    Value = string.Empty, 
                    IsQuoted = true,
                    CmdLineOrder = 2
                },
            };

            return plinkConnection;

        }

        public static ConnectionDescription Ssh()
        {
            var sshConnection = new ConnectionDescription();
            sshConnection.AppPath = "putty.exe";
            sshConnection.ConnectionTypeName = "SSH";
            sshConnection.Name = "My SSH";
            sshConnection.Parameters = new ConnectionParameterBase[]
            {
                new SwitchConnectionParameter()
                {
                    Name = "Ssh",
                    IsHidden = true, 
                    OnValue = "-ssh",
                    IsOn = true, 
                    OffValue = string.Empty, 
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.User,
                    DisplayName = "Пользователь",
                    CmdLineName = "",
                    Delimiter = "",
                    Value = "username",
                    NextParameterDelimiter = "@"
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Host,
                    DisplayName = "Хост",
                    CmdLineName = "",
                    Delimiter = "",
                    Value = "localhost",
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Port,
                    DisplayName = "Порт",
                    CmdLineName = "",
                    Delimiter = " ",
                    Value = "22"
                },

                new PuttyPasswordParameter(), 

                new SwitchConnectionParameter
                {
                    Name = "Forwarding",
                    DisplayName = "Forwarding", 
                    OnValue = "-X", 
                    OffValue = string.Empty, 
                },

                new FilePathConnectionParameter
                {
                    Name = "CommandFile", 
                    DisplayName = "Файл с командой",
                    CmdLineName = "-m",
                    Delimiter = " ",
                    Value = string.Empty,
                    IsQuoted = true,
                    CmdLineOrder = 2
                },

                new FilePathConnectionParameter
                {
                    Name = "KeyFile",
                    DisplayName = "Файл с ключом",
                    CmdLineName = "-i",
                    Delimiter = " ",
                    Value = string.Empty,
                    IsQuoted = true,
                    CmdLineOrder = 3
                },
            };

            return sshConnection;
        }

        public static ConnectionDescription Ammy()
        {
            var ammyConnection = new ConnectionDescription();
            ammyConnection.AppPath = "AA_v3.exe";
            ammyConnection.ConnectionTypeName = "Ammy Admin";
            ammyConnection.Name = "My AmmyAdmin";
            ammyConnection.Parameters = new ConnectionParameterBase[]
            {
                new StringConnectionParameter()
                {
                    Name = "ConnectID",
                    DisplayName = "ID соединения",
                    CmdLineName = "-connect",
                    Delimiter = " ",
                    Value = "",
                    CmdLineOrder = 1,
                },

                new StringConnectionParameter
                {
                    Name = KnownParameters.Password,
                    DisplayName = "Пароль",
                    CmdLineName = "-password",
                    Delimiter = " ",
                    Value = string.Empty
                },

            };

            return ammyConnection;
        }

        public static ConnectionDescription TeamViewer()
        {
            var tv = new ConnectionDescription();
            tv.AppPath = "teamviewer.exe";
            tv.ConnectionTypeName = "Team Viewer";
            tv.Name = "My TeamViewer";

            tv.Parameters = new ConnectionParameterBase[]
            {
                new StringConnectionParameter()
                {
                    Name = "ConnectID",
                    DisplayName = "ID соединения",
                    CmdLineName = "-i",
                    Delimiter = " ",
                    Value = "",
                },

                new StringConnectionParameter
                {
                    Name = KnownParameters.Password,
                    DisplayName = "Пароль",
                    CmdLineName = "-P",
                    Delimiter = " ",
                    IsQuoted = true,
                    Value = string.Empty
                },

            };

            return tv;
        }

        public static ConnectionDescription WinScpFtp()
        {
            var connection = new ConnectionDescription();
            connection.AppPath = "winscp.exe";
            connection.ConnectionTypeName = "WinScp (FTP)";
            connection.Name = "My WinSCP";
            connection.Parameters = new ConnectionParameterBase[]
            {
                new SwitchConnectionParameter()
                {
                    Name = "Protocol",
                    IsHidden = true,
                    OnValue = "ftp://",
                    IsOn = true,
                    OffValue = string.Empty,
                    NextParameterDelimiter = "",
                    CmdLineOrder = 0,
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Host,
                    DisplayName = "Хост",
                    CmdLineName = "",
                    Delimiter = "@",
                    Value = "localhost",
                    NextParameterDelimiter = "",
                    CmdLineOrder = 3,
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Port,
                    DisplayName = "Порт",
                    CmdLineName = "",
                    Delimiter = ":",
                    Value = "22", 
                    CmdLineOrder = 4,
                },

                new WinScpEncodedParameter()
                {
                    Name = KnownParameters.User,
                    DisplayName = "Пользователь",
                    CmdLineName = "",
                    Delimiter = "",
                    Value = "username",
                    NextParameterDelimiter = "", 
                    CmdLineOrder = 1, 
                },

                new WinScpEncodedParameter()
                {
                    Name = KnownParameters.Password,
                    DisplayName = "Пароль",
                    CmdLineName = "",
                    Delimiter = ":",
                    NextParameterDelimiter = "", 
                    CmdLineOrder = 2, 
                },

                new FilePathConnectionParameter
                {
                    Name = "KeyFile",
                    DisplayName = "Файл с ключом",
                    CmdLineName = "/privatekey",
                    Delimiter = "=",
                    Value = string.Empty,
                    IsQuoted = true,
                    CmdLineOrder = 6
                },

                new SwitchConnectionParameter
                {
                    Name = "Passive",
                    DisplayName = "Пассивный режим",
                    OnValue = "/passive=on",
                    OffValue = "/passive=off",
                    CmdLineOrder = 5,
                },

            };

            return connection;

        }

        public static ConnectionDescription WinScpSftp()
        {
            var winScpConnection = new ConnectionDescription();
            winScpConnection.AppPath = "winscp.exe";
            winScpConnection.ConnectionTypeName = "WinScp (SFTP)";
            winScpConnection.Name = "My WinSCP";
            winScpConnection.Parameters = new ConnectionParameterBase[]
            {
                new SwitchConnectionParameter()
                {
                    Name = "Protocol",
                    IsHidden = true,
                    OnValue = "sftp://",
                    IsOn = true,
                    OffValue = string.Empty,
                    NextParameterDelimiter = "",
                    CmdLineOrder = 0,
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Host,
                    DisplayName = "Хост",
                    CmdLineName = "",
                    Delimiter = "@",
                    Value = "localhost",
                    NextParameterDelimiter = "",
                    CmdLineOrder = 3,
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Port,
                    DisplayName = "Порт",
                    CmdLineName = "",
                    Delimiter = ":",
                    Value = "22",
                    CmdLineOrder = 4,
                },

                new WinScpEncodedParameter()
                {
                    Name = KnownParameters.User,
                    DisplayName = "Пользователь",
                    CmdLineName = "",
                    Delimiter = "",
                    Value = "username",
                    NextParameterDelimiter = "",
                    CmdLineOrder = 1,
                },

                new WinScpEncodedParameter()
                {
                    Name = KnownParameters.Password,
                    DisplayName = "Пароль",
                    CmdLineName = "",
                    Delimiter = ":",
                    NextParameterDelimiter = "",
                    CmdLineOrder = 2,
                },

                new FilePathConnectionParameter
                {
                    Name = "KeyFile",
                    DisplayName = "Файл с ключом",
                    CmdLineName = "/privatekey",
                    Delimiter = "=",
                    Value = string.Empty,
                    IsQuoted = true,
                    CmdLineOrder = 6
                },

            };

            return winScpConnection;

        }

        public static ConnectionDescription WinScpScp()
        {
            var connection = new ConnectionDescription();
            connection.AppPath = "winscp.exe";
            connection.ConnectionTypeName = "WinScp (SCP)";
            connection.Name = "My WinSCP";
            connection.Parameters = new ConnectionParameterBase[]
            {
                new SwitchConnectionParameter()
                {
                    Name = "Protocol",
                    IsHidden = true,
                    OnValue = "scp://",
                    IsOn = true,
                    OffValue = string.Empty,
                    NextParameterDelimiter = "",
                    CmdLineOrder = 0,
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Host,
                    DisplayName = "Хост",
                    CmdLineName = "",
                    Delimiter = "@",
                    Value = "localhost",
                    NextParameterDelimiter = "",
                    CmdLineOrder = 3,
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Port,
                    DisplayName = "Порт",
                    CmdLineName = "",
                    Delimiter = ":",
                    Value = "22",
                    CmdLineOrder = 4,
                },

                new WinScpEncodedParameter()
                {
                    Name = KnownParameters.User,
                    DisplayName = "Пользователь",
                    CmdLineName = "",
                    Delimiter = "",
                    Value = "username",
                    NextParameterDelimiter = "",
                    CmdLineOrder = 1,
                },

                new WinScpEncodedParameter()
                {
                    Name = KnownParameters.Password,
                    DisplayName = "Пароль",
                    CmdLineName = "",
                    Delimiter = ":",
                    NextParameterDelimiter = "",
                    CmdLineOrder = 2,
                },

                new FilePathConnectionParameter
                {
                    Name = "KeyFile",
                    DisplayName = "Файл с ключом",
                    CmdLineName = "/privatekey",
                    Delimiter = "=",
                    Value = string.Empty,
                    IsQuoted = true,
                    CmdLineOrder = 6
                },

            };

            return connection;

        }

        public static ConnectionDescription Telnet()
        {
            var connection = new ConnectionDescription();
            connection.AppPath = "";
            connection.ConnectionTypeName = "Telnet";
            connection.Name = "My Telnet";
            connection.ParameterDelimiter = "\n";

            connection.Parameters = new ConnectionParameterBase[]
            {
                new SwitchConnectionParameter()
                {
                    Name = "TelnetFlag",
                    DisplayName = "TelnetFlag",
                    OnValue = "-telnet",
                    OffValue = "",
                    IsOn = true, 
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Host,
                    DisplayName = "Хост",
                    CmdLineName = "host",
                    Delimiter = "",
                    Value = "",
                },

                new StringConnectionParameter()
                {
                    Name = KnownParameters.Port,
                    DisplayName = "Порт",
                    CmdLineName = "",
                    Delimiter = "",
                    Value = "22",

                },

                new StringConnectionParameter
                {
                    Name = KnownParameters.User,
                    DisplayName = "Пользователь",
                    CmdLineName = "",
                    Delimiter = "",
                    Value = string.Empty
                },

                new StringConnectionParameter
                {
                    Name = KnownParameters.Password,
                    DisplayName = "Пароль",
                    CmdLineName = "",
                    Delimiter = "",
                    Value = string.Empty,
                },
            };
            return connection;
        }


    }
}
