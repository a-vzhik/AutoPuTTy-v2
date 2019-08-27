using AutoPuTTY.Core;
using AutoPuTTY.Desktop.Data;
using AutoPuTTY.Desktop.Parameters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Desktop
{
    internal class ConnectionGroupViewModel : ObservableObject
    {
        private string _name;
        private bool _isExpanded;
        private readonly ConnectionGroup _connectionGroup;
        private readonly KnownConnections _knownConnections;
        private readonly ConnectionParameterViewModelFactory _connectionParameterViewModelFactory;

        public ConnectionGroupViewModel(
            ConnectionGroup connectionGroup,
            KnownConnections knownConnections,
            ConnectionParameterViewModelFactory connectionParameterViewModelFactory)
        {
            _connectionGroup = connectionGroup;
            _knownConnections = knownConnections;
            _connectionParameterViewModelFactory = new ConnectionParameterViewModelFactory();

            ConnectionViewModels = new ObservableCollection<ConnectionDescriptionViewModel>(
                connectionGroup.Connections
                   .Select(c => new ConnectionDescriptionViewModel(c, this, _knownConnections, new SelectFileView(), _connectionParameterViewModelFactory))
                   .ToList());
            
            GroupViewModels = new ObservableCollection<ConnectionGroupViewModel>(
                connectionGroup.Groups
                .Select(gr => new ConnectionGroupViewModel(gr, knownConnections, _connectionParameterViewModelFactory))
                .ToList());

            ParameterViewModels = connectionGroup.Parameters
                .Select(p => connectionParameterViewModelFactory.Create(p))
                .ToList();

            Name = connectionGroup.Name;
        }

        public ConnectionDescriptionViewModel AddConnection(ConnectionDescription connection)
        {
            var vm = new ConnectionDescriptionViewModel(connection, this, _knownConnections, new SelectFileView(), _connectionParameterViewModelFactory);
            ConnectionViewModels.Add(vm);
            Source.Connections.Add(vm.Source);

            return vm;
        }

        public void RemoveConnection(ConnectionDescriptionViewModel connectionViewModel)
        {
            ConnectionViewModels.Remove(connectionViewModel);
            Source.Connections.Remove(connectionViewModel.Source);
        }

        public IList<object> ParameterViewModels { get; }

        public string IconUri => "pack://application:,,,/Images/folder.png";

        public ConnectionGroup Source
        {
            get
            {
                return _connectionGroup;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                _connectionGroup.Name = _name;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                if (IsExpanded == value)
                    return;

                _isExpanded = value;
                RaisePropertyChanged(nameof(IsExpanded));
            }
        }
       
        public ObservableCollection<ConnectionDescriptionViewModel> ConnectionViewModels
        {
            get;
            private set;
        }

        public ObservableCollection<ConnectionGroupViewModel> GroupViewModels
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return $"Группа {this.Source.Name}";
        }

    }
}
