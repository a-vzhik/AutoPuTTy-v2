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

        public ConnectionGroupViewModel(
            ConnectionGroup connectionGroup,
            KnownConnections knownConnections,
            ConnectionParameterViewModelFactory connectionParameterViewModelFactory)
        {
            _connectionGroup = connectionGroup;

            ConnectionViewModels = new ObservableCollection<ConnectionDescriptionViewModel>(
                connectionGroup.Connections
                .Select(c => new ConnectionDescriptionViewModel(c, knownConnections, new SelectFileView(), new ConnectionParameterViewModelFactory()))
                .ToList());

            GroupViewModels = new ObservableCollection<ConnectionGroupViewModel>(
                connectionGroup.Groups
                .Select(gr => new ConnectionGroupViewModel(gr, knownConnections, new ConnectionParameterViewModelFactory()))
                .ToList());

            ParameterViewModels = connectionGroup.Parameters
                .Select(p => connectionParameterViewModelFactory.Create(p))
                .ToList();

            Name = connectionGroup.Name;
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

    }
}
