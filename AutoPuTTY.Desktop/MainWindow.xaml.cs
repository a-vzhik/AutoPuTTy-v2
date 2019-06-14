using AutoPuTTY.Core;
using AutoPuTTY.Core.Parameters;
using AutoPuTTY.Desktop.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoPuTTY.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Title = string.Format("AutoPuTTY (версия {0})", Assembly.GetExecutingAssembly().GetName().Version);
            DataContext = new MainWindowViewModel();

            InitializeComponent();

            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var mwvm = DataContext as MainWindowViewModel;

            if (mwvm != null)
            {
                mwvm.Close();
            }
        }
    }
}
