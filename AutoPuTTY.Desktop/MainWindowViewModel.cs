using AutoPuTTY.Core;
using AutoPuTTY.Desktop.Data;
using AutoPuTTY.Desktop.Input;
using AutoPuTTY.Desktop.Parameters;
using AutoPuTTY.Repository;
using AutoPuTTY.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Input;

using Timer = System.Timers.Timer;
namespace AutoPuTTY.Desktop
{
    internal class MainWindowViewModel : ObservableObject
    {
        private static string ConnectionsFile = "project.json";
        private static string ProfilesFile = "profiles.json";

        private readonly Dictionary<string, Func<ConnectionDescription>> _knownConnectionFactories = new Dictionary<string, Func<ConnectionDescription>>();
        private readonly KnownConnections _knownConnections;

        private DelegateCommand _createGroupCommand;

        private DelegateCommand<string> _createConnectionCommand;
        private DelegateCommand<object> _deleteObjectCommand;
        private object _selectedObject;
        private readonly Timer _timer;

        SynchronizationContext _context = SynchronizationContext.Current;
        private DelegateCommand<ConnectionDescriptionViewModel> _runConnectionCommand;
        private DelegateCommand<ConnectionDescriptionViewModel> _copyConnectionCommand;

        public MainWindowViewModel()
        {
            _knownConnections = new KnownConnections();
            ConnectionGroupViewModels = new ObservableCollection<ConnectionGroupViewModel>();

            var newRepo = new NewRepositoryReader(_knownConnections);
            newRepo.LoadProfiles("profiles.json");

            var oldRepo = new OldRepositoryReader("autoputty.xml", _knownConnections);
            var groups = oldRepo.Load();

            if (groups != null)
            {
                newRepo.SaveProfiles(ProfilesFile, _knownConnections._knownConnectionProfiles);
                newRepo.Save(ConnectionsFile, groups);
            }
            else
            {
                groups = newRepo.Load(ConnectionsFile);
            }

            if (groups != null)
            {
                foreach (var gr in groups)
                {
                    ConnectionGroupViewModels.Add(new ConnectionGroupViewModel(gr, _knownConnections, new ConnectionParameterViewModelFactory()));
                }

                SelectedObject = ConnectionGroupViewModels.FirstOrDefault();
            }

            _timer = new Timer();
            _timer.Interval = 10000;
            _timer.Elapsed += Timer_Elapsed;
            _timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _context.Send(obj =>
            {
                var groups = ConnectionGroupViewModels.Select(vm => vm.Source).ToList();

                var repoReader = new NewRepositoryReader(_knownConnections);
                repoReader.Save(ConnectionsFile, groups);
                repoReader.SaveProfiles(ProfilesFile, _knownConnections._knownConnectionProfiles);
            }, null);
        }

        public ICommand CreateGroupCommand
        {
            get
            {
                if (_createGroupCommand == null) {
                    _createGroupCommand = new DelegateCommand(() =>
                    {
                        var group = new ConnectionGroup()
                        {
                            Connections = new List<ConnectionDescription>(),
                            Name = "Новая группа"
                        };

                        var viewModel = new ConnectionGroupViewModel(group, _knownConnections, new ConnectionParameterViewModelFactory());

                        ConnectionGroupViewModels.Add(viewModel);
                        SelectedObject = viewModel;
                    });
                }

                return _createGroupCommand;
            }
        }

        public ICommand CreateConnectionCommand
        {
            get
            {
                if (_createConnectionCommand == null)
                {
                    _createConnectionCommand = new DelegateCommand<string>(connectionType =>
                    {
                        var selectedGroup = (ConnectionGroupViewModel)SelectedObject;
                        selectedGroup.IsExpanded = true;

                        var connection = _knownConnections.CreateFromProfile(connectionType);
                        connection.Name = $"Новое подключение {connection.ConnectionTypeName}";

                        foreach (var groupParam in selectedGroup.Source.Parameters)
                        {
                            if (!string.IsNullOrEmpty(groupParam.Value))
                            {
                                var connectionParam = connection.Parameters.FirstOrDefault(p => p.Name == groupParam.Name);
                                if (connectionParam != null)
                                {
                                    connectionParam.Value = groupParam.Value;
                                }
                            }
                        }

                        var viewModel = selectedGroup.AddConnection(connection);
                        SelectedObject = viewModel;
                    }, 
                    _ => SelectedObject is ConnectionGroupViewModel,
                    true);
                }

                return _createConnectionCommand;
            }
        }

        public ICommand RunConnectionCommand
        {
            get
            {
                if (_runConnectionCommand == null)
                {
                    _runConnectionCommand = new DelegateCommand<ConnectionDescriptionViewModel>(
                        m => m.RunCommand.Execute(null), true);
                }

                return _runConnectionCommand;
            }
        }

        public ICommand CopyConnectionCommand
        {
            get
            {
                if (_copyConnectionCommand == null)
                {
                    _copyConnectionCommand = new DelegateCommand<ConnectionDescriptionViewModel>(vm =>
                    {
                        ConnectionDescription newConnection = CopyConnection(vm.Source);

                        var newConnectionViewModel = vm.Parent.AddConnection(newConnection);
                        SelectedObject = newConnectionViewModel;
                    }, true);
                }

                return _copyConnectionCommand;
            }
        }

        private ConnectionDescription CopyConnection(ConnectionDescription source)
        {
            var newConnection = _knownConnections.CreateFromProfile(source.ConnectionTypeName);

            newConnection.Name = source.Name + " (Копия)";

            foreach (var oldParam in source.Parameters)
            {
                var copyParam = newConnection.Parameters.FirstOrDefault(p => p.Name == oldParam.Name);
                if (copyParam != null)
                {
                    copyParam.Value = oldParam.Value;
                }
            }

            return newConnection;
        }

        public ICommand DeleteObjectCommand
        {
            get
            {
                if (_deleteObjectCommand == null)
                {
                    _deleteObjectCommand = new DelegateCommand<object>(obj =>
                    {
                        if (obj is ConnectionGroupViewModel)
                        {
                            DeleteGroup((ConnectionGroupViewModel)obj);
                        }
                        else
                        {
                            DeleteConnection((ConnectionDescriptionViewModel)obj);
                        }
                    },
                    obj => true,
                    true);
                }
                return _deleteObjectCommand;    
            }
        }

        private void DeleteConnection(ConnectionDescriptionViewModel obj)
        {
            var pair = ConnectionGroupViewModels
                .SelectMany(g => g.ConnectionViewModels.Select(c => new  { Group = g, Connection = c }))
                .Where(anon => anon.Connection == obj)
                .FirstOrDefault();

            if (pair == null)
                return;

            obj.Source.Profile = null;
            var isSelected = obj == SelectedObject;
            int index = 0;
            if (isSelected)
            {
                index = pair.Group.ConnectionViewModels.IndexOf(obj);
            }

            pair.Group.RemoveConnection(pair.Connection);

            if (isSelected)
            {
                if (pair.Group.ConnectionViewModels.Count == index)
                {
                    index--;
                }
                if (index >= 0)
                {
                    SelectedObject = pair.Group.ConnectionViewModels[index];
                }
                else
                {
                    SelectedObject = pair.Group;
                }
            }

        }

        private void DeleteGroup(ConnectionGroupViewModel obj)
        {
            var isSelected = obj == SelectedObject;
            int index = 0;
            if (isSelected)
            {
                index = ConnectionGroupViewModels.IndexOf(obj);
            }

            ConnectionGroupViewModels.Remove(obj);
            if (isSelected )
            {
                if (ConnectionGroupViewModels.Count == index)
                {
                    index--;
                }
                if (index >= 0)
                {
                    SelectedObject = ConnectionGroupViewModels[index];
                }
                else
                {
                    SelectedObject = null;
                }
            }
        }

        public ObservableCollection<ConnectionGroupViewModel> ConnectionGroupViewModels { get; set; }

        public object SelectedObject
        {
            get
            {
                return _selectedObject;
            }
            set
            {
                _selectedObject = value;
                ViewModelCommandHelper.Invalidate(_createConnectionCommand);
                RaisePropertyChanged(nameof(SelectedObject));
            }
        }

        public IEnumerable<string> ConnectionTypes
        {
            get
            {
                return _knownConnections._knownConnectionProfiles.Keys;
            }
        }

    }
}
