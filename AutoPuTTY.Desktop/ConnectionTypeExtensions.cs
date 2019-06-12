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
                    return "Remote Desktop";
                case ConnectionType.Vnc:
                    return "Vnc";
                case ConnectionType.Scp:
                    return "WinScp (SCP)";
                case ConnectionType.Ftp:
                    return "WinScp (FTP)";
                case ConnectionType.Sftp:
                    return "WinScp (SFTP)";
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
