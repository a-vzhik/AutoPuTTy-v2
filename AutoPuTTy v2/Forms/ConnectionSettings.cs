using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using AutoPuTTY.Forms.Popups;
using AutoPuTTY.Properties;
using AutoPuTTY.Utils;
using AutoPuTTY.Utils.Data;

namespace AutoPuTTY.Forms
{
    public partial class connectionSettings : Form
    {
        private ServerElement connection = null;

        #region Conts Init

        private readonly formMain MainForm;
        private bool FirstRead;
        public bool ImportEmpty;

        public readonly object locker = new object();
        public string ImportReplace = "";

        #endregion

        #region Element Loading

        public connectionSettings(formMain form)
        {
            MainForm = form;
            InitializeComponent();
            tabPages.Add(ConnectionType.SshPuTTy, tabSettings.TabPages[0]);
            tabPages.Add(ConnectionType.Rdp, tabSettings.TabPages[1]);
            tabSettings.TabPages.Clear();

            Settings.Default.ocryptkey = Settings.Default.cryptkey;

            FirstRead = false;
        }

        #endregion

        private void bNCPath_Click(object sender, EventArgs e)
        {

           
        }

        private void bNCCommand_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseFile = new OpenFileDialog
            {
                Title = Resources.formOptions_bPuTTYExecute_Click_Select_commands_file,
                Filter = Resources.formOptions_bGImport_Click_TXT_File____txt____txt
            };

            tbNCCommand.Text = browseFile.ShowDialog() == DialogResult.OK ? browseFile.FileName : "";
        }

        private void cbNCExecuteCommand_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ncexecute = cbNCExecuteCommand.Checked;
            if (!FirstRead) MainForm.XmlHelper.configSet("ncexecute", Settings.Default.ncexecute.ToString());

            if (Settings.Default.ncexecute)
            {
                tbNCCommand.Enabled = true;
                bNCCommand.Enabled = true;
            }
            else
            {
                tbNCCommand.Enabled = false;
                bNCCommand.Enabled = false;
            }
        }

        private void tbNCPath_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ncpath = tbNCPath.Text;
            if (!FirstRead) MainForm.XmlHelper.configSet("ncpath", Settings.Default.ncpath);
        }

        private void tbNCCommand_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.nccommand = tbNCCommand.Text;
            if (!FirstRead) MainForm.XmlHelper.configSet("nccommand", Settings.Default.nccommand);
        }

        private void bPlinkPath_Click(object sender, EventArgs e)
        {
           
        }

        private void tbPlinkPath_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.plinkpath = tbPlinkPath.Text;
            if (!FirstRead) MainForm.XmlHelper.configSet("plinkpath", Settings.Default.plinkpath);
        }

        private void connectionSettings_Load(object sender, EventArgs e)
        {
            if (connection == null || connection.Type != ConnectionType.Rdp)
            {
                Close();
                return;
            }
        }

        Dictionary<ConnectionType, TabPage> tabPages = new Dictionary<ConnectionType, TabPage>();

        public void ConnectionChanged(ServerElement currentServer)
        {
            if (currentServer == null || currentServer.Type != ConnectionType.Rdp)
            {
                return;
            }

            connection = currentServer;

            gbRDP.Text = currentServer.Name;

            tabSettings.TabPages.Clear();
            tabSettings.TabPages.Add(tabPages[currentServer.Type]);

            FirstRead = true;

            cbRDPAdmin.Checked = false;
            cbRDPDrivers.Checked = false;
            cbRDPSpan.Checked = false;

            if (File.Exists(Settings.Default.cfgpath))
            {
                bool value;
                if (bool.TryParse(MainForm.XmlHelper.configGet(connection.Id + "_rdadmin"), out value))
                {
                    cbRDPAdmin.Checked = value;
                }

                if (bool.TryParse(MainForm.XmlHelper.configGet(connection.Id + "_rddrives"), out value))
                {
                    cbRDPDrivers.Checked = value;
                }
                if (bool.TryParse(MainForm.XmlHelper.configGet(connection.Id + "_rdspan"), out value))
                {
                    cbRDPSpan.Checked = value;
                }
                cbRDPSize.Text = MainForm.XmlHelper.configGet(connection.Id + "_rdsize");
            }
            FirstRead = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbRDPSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList arraylist = new ArrayList();
            string[] size = cbRDPSize.Text.Split('x');

            foreach (string width in size)
            {
                if (Int32.TryParse(width.Trim(), out _)) arraylist.Add(width.Trim());
            }
            var value = string.Empty;
            if (arraylist.Count == 2 || cbRDPSize.Text.Trim() == cbRDPSize.Items[cbRDPSize.Items.Count - 1].ToString())
            {
                value = cbRDPSize.Text.Trim();
            }
            
            if (!FirstRead) {
                MainForm.XmlHelper.configSet(connection.Id + "_rdsize", value);
            }
        }

        private void cbRDPAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (!FirstRead)
            {
                MainForm.XmlHelper.configSet(connection.Id + "_rdadmin", cbRDPAdmin.Checked.ToString());
            }
        }

        private void cbRDPDrivers_CheckedChanged(object sender, EventArgs e)
        {
            if (!FirstRead)
            {
                MainForm.XmlHelper.configSet(connection.Id + "_rddrives", cbRDPDrivers.Checked.ToString());
            }
        }        
        private void cbRDPSpan_CheckedChanged(object sender, EventArgs e)
        {
            if (!FirstRead)
            {
                MainForm.XmlHelper.configSet(connection.Id + "_rdspan", cbRDPSpan.Checked.ToString());
            }
            cbRDPSize.Enabled = !cbRDPSpan.Checked;
        }
    }
}