using AutoPuTTY.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPuTTY.Utils.Data
{
    public static class ConnectionTypeExtensions
    {
        public static string ConvertToString(this ConnectionType connectionType)
        {
            switch (connectionType)
            {
                case ConnectionType.SshPuTTy:
                    return "SSH";
                case ConnectionType.Rdp:
                    return StringResources.formMain_cbType_SelectedIndexChanged_Remote_Desktop;
                case ConnectionType.Vnc:
                    return "VNC";
                case ConnectionType.Scp:
                    return "WinSCP (SCP)";
                case ConnectionType.Ftp:
                    return "WinSCP (FTP)";
                case ConnectionType.Sftp:
                    return "WinSCP (SFTP)";
                case ConnectionType.Telnet:
                    return "Telnet";
                case ConnectionType.Plink:
                    return "Plink";
                case ConnectionType.RAdmin:
                    return "RAdmin";
                case ConnectionType.AmmyAdmin:
                    return "Ammy Admin";
                case ConnectionType.TeamViewer:
                    return "TeamViewer";
                case ConnectionType.QuasarMsc:
                    return "Quasar-msc";
                case ConnectionType.FileZillaFtp:
                    return "FileZilla FTP";
                case ConnectionType.FarNetbox:
                    return "FAR-netbox";
                default:
                    return connectionType.ToString(); 
            }
    }
    }
}
