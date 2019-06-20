using AutoPuTTY.Core;
using AutoPuTTY.Desktop.Data;
using AutoPuTTY.Desktop.Input;
using AutoPuTTY.Desktop.Parameters;
using AutoPuTTY.Desktop.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace AutoPuTTY.Desktop
{
    internal class ConnectionDescriptionViewModel : ObservableObject
    {
        private readonly KnownConnections _knownConnections;
        private readonly IFileSelector _fileSelector;
        private readonly Timer _autoCheckTimer;
        private readonly TaskScheduler _uiScheduler;

        private string _name;

        private DelegateCommand _tracertCommand;
        private DelegateCommand _runCommand;
        private DelegateCommand _netCatCommand;
        private DelegateCommand _pingCommand;

        private Uri _portAccessibilityIconUri;
        private Uri _pingAccessibilityIconUri;

        private Task _updatePingAccessibilityTask;
        private Task _updatePortAccessibilityTask;
        private bool _isSelected;
        private readonly NetworkService _networkService;
        private ConnectionParameterViewModelFactory _connectionParameterViewModelFactory;

        public ConnectionDescriptionViewModel(
            ConnectionDescription source,
            ConnectionGroupViewModel parent,
            KnownConnections knownConnections,
            IFileSelector fileSelector,
            ConnectionParameterViewModelFactory connectionParameterViewModelFactory)
        {
            Parent = parent;
            _knownConnections = knownConnections;
            _fileSelector = fileSelector;

            _autoCheckTimer = new Timer(1000);
            _autoCheckTimer.Elapsed += AutoCheckTimer_Elapsed;
            _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            _networkService = new NetworkService();
            _connectionParameterViewModelFactory = connectionParameterViewModelFactory;

            PingAccessibilityIconUri = new Uri("pack://application:,,,/Images/gray-icon.png");
            PortAccessibilityIconUri = new Uri("pack://application:,,,/Images/gray-icon.png");

            InitFromSource(source);

            ConnectionTypes = _knownConnections._knownConnectionProfiles.Keys.Except(new[] { "NetCat" }).ToList();
            SelectedConnectionType = Source.ConnectionTypeName;
        }

        private void InitFromSource(ConnectionDescription source)
        {
            Source = source;
            Name = source.Name;

            ParameterViewModels = source.Parameters
                .Select(p => _connectionParameterViewModelFactory.Create(p))
                .ToList();
            RaisePropertyChanged(nameof(ParameterViewModels));

            foreach (var pvm in ParameterViewModels.OfType<ObservableObject>())
            {
                pvm.PropertyChanged += ParameterViewModel_PropertyChanged;
            }

            UpdateCommandLineText();
            IsAutoCheckEnabled = Source.IsAutoCheckEnabled;

            RaisePropertyChanged(nameof(IconUri));
        }

        private void AutoCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdatePingAccesibility();
            UpdatePortAccesibility();
        }

        private void ParameterViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateCommandLineText();
        }

        private void UpdateCommandLineText()
        {
            RaisePropertyChanged(nameof(CommandLineText));
        }

        internal ConnectionGroupViewModel Parent { get; }

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

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged(nameof(IsSelected));

                    _autoCheckTimer.Enabled = IsSelected && IsAutoCheckEnabled;
                }
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
                        var netCat = _knownConnections.CreateFromProfile("NetCat");
                        netCat.UpdateParameterIfExists(KnownParameters.Host, Source.FindParam(KnownParameters.Host));
                        netCat.UpdateParameterIfExists(KnownParameters.Port, Source.FindParam(KnownParameters.Port));

                        StartConnection(netCat);
                    });
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
                    _runCommand = new DelegateCommand(() => StartConnection(Source));
                }
                return _runCommand;

            }
        }

        public void StartConnection(ConnectionDescription connection)
        {
            if (!File.Exists(connection.Profile.AppPath))
            {
                var newPath = _fileSelector.SelectFile(connection.Profile, $"Select {connection.ConnectionTypeName} executable", "EXE File (*.exe)|*.exe");
                if (string.IsNullOrEmpty(newPath))
                {
                    return;
                }

                connection.Profile.AppPath = newPath;
                connection.AppPath = newPath;

                RaisePropertyChanged(nameof(Source));
                UpdateCommandLineText();
            }

            new ConnectionLauncherProvider().GetLauncher(connection).Run();
        }

        public IList<object> ParameterViewModels { get; private set; }

        public Uri IconUri => KnownConnections.GetIconUrl(Source.ConnectionTypeName);

        public Uri PortAccessibilityIconUri
        {
            get
            {
                return _portAccessibilityIconUri; 
            }
            set
            {
                _portAccessibilityIconUri = value;
                RaisePropertyChanged(nameof(PortAccessibilityIconUri)); 
            }
        }

        public Uri PingAccessibilityIconUri
        {
            get
            {
                return _pingAccessibilityIconUri; 
            }
            set
            {
                _pingAccessibilityIconUri = value;
                RaisePropertyChanged(nameof(PingAccessibilityIconUri));
            }
        }

        public string CommandLineText
        {
            get
            {
                ConnectionCmdLineGenerator gen = new ConnectionCmdLineGenerator(Source);
                return gen.GetCommandLine();
            }
        }

        public IList<string> ConnectionTypes
        {
            get;
            private set;
        }

        public string SelectedConnectionType
        {
            get
            {
                return Source.ConnectionTypeName;
            }
            set
            {
                if (Source.ConnectionTypeName != value)
                {
                    Source.ConnectionTypeName = value;

                    var newConnection = _knownConnections.CreateFromProfile(value);
                    newConnection.Name = Source.Name;
                    newConnection.UpdateParameterIfExists(KnownParameters.Host, Source.FindParam(KnownParameters.Host));
                    newConnection.UpdateParameterIfExists(KnownParameters.Port, Source.FindParam(KnownParameters.Port));
                    newConnection.UpdateParameterIfExists(KnownParameters.User, Source.FindParam(KnownParameters.User));
                    newConnection.UpdateParameterIfExists(KnownParameters.Password, Source.FindParam(KnownParameters.Password));

                    Parent.Source.ReplaceConnection(Source, newConnection);
                        
                    InitFromSource(newConnection);
                    RaisePropertyChanged(nameof(SelectedConnectionType));

                }
            }
        }

        public bool IsAutoCheckEnabled
        {
            get
            {
                return Source.IsAutoCheckEnabled;
            }
            set
            {
                Source.IsAutoCheckEnabled = value;
                RaisePropertyChanged(nameof(IsAutoCheckEnabled));

                _autoCheckTimer.Enabled = IsAutoCheckEnabled && IsSelected;
                if (!IsAutoCheckEnabled)
                {
                    PingAccessibilityIconUri = new Uri("pack://application:,,,/Images/gray-icon.png");
                    PortAccessibilityIconUri = new Uri("pack://application:,,,/Images/gray-icon.png");
                }
            }
        }

        private void UpdatePingAccesibility()
        {
            if (_updatePingAccessibilityTask != null && !_updatePingAccessibilityTask.IsCompleted)
            {
                Trace.WriteLine("Ping task still running.");
                return;
            }

            _updatePingAccessibilityTask = _networkService
                .GetPingAccessibility(Source.FindParam(KnownParameters.Host))
                .ContinueWith(t =>
                {
                    // By the time we reach this synchronous code - it's possible that 
                    // auto check is disabled and we don't care about the result.
                    if (IsAutoCheckEnabled)
                    {
                        PingAccessibilityIconUri = t.IsFaulted || !t.Result
                            ? new Uri("pack://application:,,,/Images/red-icon.png")
                            : new Uri("pack://application:,,,/Images/green-icon.png");
                    }

                }, _uiScheduler);            
        }

        private void UpdatePortAccesibility()
        {
            if (_updatePortAccessibilityTask != null && !_updatePortAccessibilityTask.IsCompleted)
            {
                return;
            }

            _updatePortAccessibilityTask = _networkService
                .GetPortAccessibility(Source.FindParam(KnownParameters.Host), Source.FindParam(KnownParameters.Port))
                .ContinueWith(t =>
                {
                    // By the time we reach this synchronous code - it's possible that 
                    // auto check is disabled and we don't care about the result.
                    if (IsAutoCheckEnabled)
                    {
                        PortAccessibilityIconUri = t.IsFaulted || !t.Result
                            ? new Uri("pack://application:,,,/Images/red-icon.png")
                            : new Uri("pack://application:,,,/Images/green-icon.png");
                    }
                }, _uiScheduler);
        }
    }
}