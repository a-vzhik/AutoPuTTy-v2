using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoPuTTY.Properties;
using AutoPuTTY.Utils;

namespace AutoPuTTY.Forms.Options
{
    partial class CommonOptionsView : UserControl
    {
        public CommonOptionsView(CommonOptionsViewModel commonOptionsViewModel)
        {
            Model = commonOptionsViewModel;

            InitializeComponent();

            tbAppPath.Text = Model.AppPath;
            bChangePath.Click += bChangePath_Click;

            lAppPath.Text = string.Format("{0} ({1})", lAppPath.Text, Model.AppPath.Split('\\').Last()); 
        }

        public CommonOptionsViewModel Model { get; private set; }

        private void bChangePath_Click(object sender, EventArgs e)
        {
            if (Model.ChangeAppPath())
            {
                tbAppPath.Text = Model.AppPath;
            }
        }
    }
}
