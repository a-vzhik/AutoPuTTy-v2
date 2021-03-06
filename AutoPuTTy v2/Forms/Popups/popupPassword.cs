using System;
using System.Windows.Forms;
using AutoPuTTY.Utils;

namespace AutoPuTTY.Forms.Popups
{
    public partial class popupPassword : Form
    {
        #region Conts Init

        public bool auth;
        public string cpassword = "";
        private readonly string password;
        private int i;

        #endregion

        #region Element Init

        public popupPassword(string xmlpassword)
        {
            InitializeComponent();
            password = xmlpassword;
        }

        #endregion

        #region Element Events

        private void bOK_Click(object sender, EventArgs e)
        {
            if (CryptHelper.Encrypt(tbPassword.Text, Properties.Settings.Default.pcryptkey) == password)
            {
                auth = true;
                Close();
            }
            else
            {
                tbPassword.Text = "";
                switch (i)
                {
                    case 0:
                    case 4:
                        tText.Text = "You failed, try again...";
                        break;
                    case 1:
                        tText.Text = "You failed again, looks like you lost it...";
                        break;
                    case 2:
                        tText.Text = "You'll have to restart from scratch...";
                        break;
                    case 3:
                        tText.Text = "Ahahahah :)";
                        break;
                }
                i++;
            }
        }

        private void bQuit_Click(object sender, EventArgs e)
        {
            if (!auth && cpassword == "") Application.Exit();
        }

        #endregion
    }
}