using AutoPuTTY.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoPuTTY.Utils
{
    public class FileSelector
    {
        public string SelectFile(string title, string filter)
        {
            OpenFileDialog browseFile = new OpenFileDialog
            {
                Title = title,
                Filter = filter
            };

            if (browseFile.ShowDialog() == DialogResult.OK)
            {
                if (browseFile.FileName.Contains(" "))
                    browseFile.FileName = "\"" + browseFile.FileName + "\"";
                return browseFile.FileName;
            }

            return null;
        }
    }
}
