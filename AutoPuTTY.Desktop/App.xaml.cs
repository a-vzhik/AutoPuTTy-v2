using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace AutoPuTTY.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                File.WriteAllText("exception.txt", JsonConvert.SerializeObject(e.ExceptionObject, Formatting.Indented));
            }
            catch (Exception)
            {
            }

            MessageBox.Show("Произошла непредвиденная ошибка. Подробности в файле exception.txt", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
