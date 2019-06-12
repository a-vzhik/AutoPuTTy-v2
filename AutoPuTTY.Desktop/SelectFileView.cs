using AutoPuTTY.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoPuTTY.Desktop
{
    internal class SelectFileView : IFileSelector
    {
        public string SelectFile(ConnectionDescription connection, string title, string filter)
        {
            if (MessageBox.Show("Не могу найти файл \"" + connection.AppPath + "\"\nВы хотите изменить конфигурацию?",
                    "Ошибка запуска", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                var browseFile = new OpenFileDialog
                {
                    Title = title,
                    Filter = filter
                };

                if (browseFile.ShowDialog() == DialogResult.OK)
                {
                    //if (browseFile.FileName.Contains(" "))
                    //    browseFile.FileName = "\"" + browseFile.FileName + "\"";
                    return browseFile.FileName;
                }
            }

            return null;
        }
    }
}
