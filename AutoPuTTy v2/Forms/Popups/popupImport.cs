using System;
using System.Threading;
using System.Windows.Forms;

namespace AutoPuTTY.Forms.Popups
{
    public partial class popupImport : Form
    {
        #region Const Init

        public formOptions optionsform;
        private readonly int oheight;

        #endregion

        #region Element Init

        public popupImport(formOptions form)
        {
            optionsform = form;
            InitializeComponent();

            oheight = Height;
        }

        #endregion

        #region Element Events

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            bCancel_Click(sender, e);
            if (e.CloseReason != CloseReason.UserClosing && bCancel.Text == "Cancel") e.Cancel = true;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            lock (optionsform.locker)
            {
                Monitor.Pulse(optionsform.locker);
            }
            optionsform.bwProgress.CancelAsync();
        }

        private void bReplace_Click(object sender, EventArgs e)
        {
            SwitchDuplicateWarning(false);
            optionsform.ImportReplace = "replace";
            lock (optionsform.locker)
            {
                Monitor.Pulse(optionsform.locker);
            }
        }

        private void bSkip_Click(object sender, EventArgs e)
        {
            SwitchDuplicateWarning(false);
            optionsform.ImportReplace = "skip";
            lock (optionsform.locker)
            {
                Monitor.Pulse(optionsform.locker);
            }
        }

        #endregion

        #region Methods

        private void SwitchDuplicateWarning(bool s)
        {
            if (bReplace.InvokeRequired) Invoke(new MethodInvoker(delegate
            {
                if (s) Height = oheight + pWarning.Height;
                else Height = oheight;
                pWarning.Visible = s;
                bReplace.Enabled = s;
                bSkip.Enabled = s;
            }));
            else
            {
                if (s) Height = oheight + pWarning.Height;
                else Height = oheight;
                pWarning.Visible = s;
                bReplace.Enabled = s;
                bSkip.Enabled = s;
            }
        }

        private void SwitchEmptyWarning(string n)
        {
            if (pWarning.InvokeRequired) Invoke(new MethodInvoker(delegate
            {
                if (!pWarning.Visible)
                {
                    Height = oheight + pWarning.Height;
                    pWarning.Visible = true;
                }
                lWarning.Text = n;
            }));
            else
            {
                if (!pWarning.Visible)
                {
                    Height = oheight + pWarning.Height;
                    pWarning.Visible = true;
                }
                lWarning.Text = n;
            }
        }

        public void ImportProgress(string[] args)
        {
            if (Convert.ToInt32(args[0]) < 0) args[0] = "0";
            pbProgress.Value = Convert.ToInt32(args[0]);
            lProgressValue.Text = args[1];
            lAddedValue.Text = args[2];
            lReplacedValue.Text = args[3];
            lSkippedValue.Text = args[4];
        }

        public void ImportComplete()
        {
            Text = "Import complete";
            bCancel.Text = "OK";
            bCancel.DialogResult = DialogResult.Cancel;
            if (optionsform.ImportEmpty) SwitchEmptyWarning("No entry found in file");
            else SwitchDuplicateWarning(false);
        }

        #endregion
    }
}
