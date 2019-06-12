using AutoPuTTY.Core;
using AutoPuTTY.Desktop.Data;
using AutoPuTTY.Desktop.Input;
using AutoPuTTY.Desktop.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace AutoPuTTY.Desktop
{
    public class ConnectionDescriptionViewModel : ObservableObject
    {
        private string _name;

        private DelegateCommand _runCommand;

        private DelegateCommand _netCatCommand;

        private DelegateCommand _pingCommand;

        private readonly KnownConnections _knownConnections;

        private readonly IFileSelector _fileSelector;

        public ConnectionDescriptionViewModel(
            ConnectionDescription source, 
            KnownConnections knownConnections,
            IFileSelector fileSelector,
            ConnectionParameterViewModelFactory connectionParameterViewModelFactory)
        {
            _knownConnections = knownConnections;
            _fileSelector = fileSelector;
            Source = source;
            Name = source.Name;
            ParameterViewModels = source.Parameters
                .Select(p => connectionParameterViewModelFactory.Create(p))
                .ToList();

            foreach (var pvm in ParameterViewModels.OfType<ObservableObject>())
            {
                pvm.PropertyChanged += ParameterViewModel_PropertyChanged;
            }

            UpdateCommandLineText();
        }

        private void ParameterViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateCommandLineText();
        }

        private void UpdateCommandLineText()
        {
            RaisePropertyChanged(nameof(CommandLineText));
        }

        public ConnectionDescription Source { get; private set; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                Source.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public ICommand PingCommand
        {
            get
            {
                if (_pingCommand == null)
                {
                    _pingCommand = new DelegateCommand(() =>
                    {
                        var hostParam = Source.Parameters.FirstOrDefault(p => p.Name == KnownParameters.Host);
                        if (hostParam != null)
                        {
                            var host = (hostParam.Value ?? "").Split(new[] { ':' }).FirstOrDefault();
                            if (!string.IsNullOrEmpty(host))
                            {
                                string strCmdText = $"/C ping -t {host} &pause";
                                Process.Start("CMD.exe", strCmdText);
                            }
                        }
                    });
                }

                return _pingCommand;
            }
        }

        private DelegateCommand _tracertCommand;

        public ICommand TracertCommand
        {
            get
            {
                if (_tracertCommand == null)
                {
                    _tracertCommand = new DelegateCommand(() =>
                    {
                        var hostParam = Source.Parameters.FirstOrDefault(p => p.Name == KnownParameters.Host);
                        if (hostParam != null)
                        {
                            var host = hostParam.Value;
                            if (!string.IsNullOrEmpty(host))
                            {
                                string strCmdText = "/C tracert " + host + " &pause";
                                Process.Start("CMD.exe", strCmdText);
                            }
                        }
                    });
                }

                return _tracertCommand;
            }
        }

        public ICommand NetCatCommand
        {
            get
            {
                if (_netCatCommand == null)
                {
                    _netCatCommand = new DelegateCommand(() =>
                    {
                        var ncPath = "nc64.exe";
                        var hostParam = Source.Parameters.FirstOrDefault(p => p.Name == KnownParameters.Host);
                        var portParam = Source.Parameters.FirstOrDefault(p => p.Name == KnownParameters.Port);

                        if (hostParam != null && portParam != null)
                        {
                            string strCmdText = $"/C {ncPath} -zv {hostParam.Value} {portParam.Value} &pause";
                            Process.Start("CMD.exe", strCmdText);
                        }
                    }, 
                    () => false);

                }

                return _netCatCommand;
            }
        }

        public ICommand RunCommand
        {
            get
            {
                if (_runCommand == null)
                {
                    _runCommand = new DelegateCommand(() =>
                    {
                        if (!File.Exists(Source.Profile.AppPath))
                        {
                            var newPath = _fileSelector.SelectFile(Source.Profile, $"Select {Source.ConnectionTypeName} executable", "EXE File (*.exe)|*.exe");
                            if (string.IsNullOrEmpty(newPath))
                            {
                                return;
                            }

                            Source.Profile.AppPath = newPath;
                            Source.AppPath = newPath;

                            RaisePropertyChanged(nameof(Source));
                            UpdateCommandLineText();
                        }

                        new ConnectionLauncherProvider().GetLauncher(Source).Run();
                    });
                }
                return _runCommand;

            }
        }

        public IList<object> ParameterViewModels { get; }

        public Uri IconUri => KnownConnections.GetIconUrl(Source.ConnectionTypeName);

        public string CommandLineText
        {
            get
            {
                ConnectionCmdLineGenerator gen = new ConnectionCmdLineGenerator(Source);
                return gen.GetCommandLine();
            }
        }
    }
}