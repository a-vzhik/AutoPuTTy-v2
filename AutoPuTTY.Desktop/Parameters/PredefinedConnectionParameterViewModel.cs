using AutoPuTTY.Core;
using AutoPuTTY.Core.Parameters;
using AutoPuTTY.Desktop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Desktop.Parameters
{
    internal sealed class PredefinedConnectionParameterViewModel : ConnectionParameterViewModelBase<PredefinedConnectionParameter>
    {
        private string _selectedValue;

        public PredefinedConnectionParameterViewModel(PredefinedConnectionParameter connection) : 
            base (connection)
        {
            AllowedValues = new List<string>();
            AllowedValues.Add(string.Empty);
            foreach (var v in connection.AllowedValues)
            {
                AllowedValues.Add(v);
            }

            SelectedValue = connection.Value;
        }

        public IList<string> AllowedValues { get; private set; }

        public string SelectedValue
        {
            get
            {
                return _selectedValue; 
            }
            set
            {
                ErrorInfo error;
                if (Source.TrySetValue(value, out error))
                {
                    _selectedValue = value;
                    RaisePropertyChanged(nameof(SelectedValue));
                }
            }
        }
    }
}
