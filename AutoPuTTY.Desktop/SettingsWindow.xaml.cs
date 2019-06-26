using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AutoPuTTY.Core;

namespace AutoPuTTY.Desktop
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(KnownConnections knownConnections)
        {
            Models = knownConnections._knownConnectionProfiles.ToDictionary(
                pair => pair.Key,
                pair => ToProfileSettingsVM(pair.Value));

            InitializeComponent();

            DataContext = Models;
        }

        private ProfileSettingsViewModel ToProfileSettingsVM(ConnectionDescription value)
        {
            return new ProfileSettingsViewModel(value);
        }

        internal IDictionary<string, ProfileSettingsViewModel> Models
        {
            get;
            private set;
        }
    }
}
