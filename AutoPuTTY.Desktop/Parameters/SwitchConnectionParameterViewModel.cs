using AutoPuTTY.Core;
using AutoPuTTY.Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Desktop.Parameters
{
    internal class SwitchConnectionParameterViewModel : ConnectionParameterViewModelBase<SwitchConnectionParameter>
    {
        private bool _isSelected;

        public SwitchConnectionParameterViewModel(SwitchConnectionParameter source)
            : base(source)
        {
            IsSelected = source.Value == source.OnValue;
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                ErrorInfo error;
                var paramValue = value ? Source.OnValue : Source.OffValue;

                if (Source.TrySetValue(paramValue, out error))
                {
                    _isSelected = value;
                    Source.IsOn = value;
                    RaisePropertyChanged(nameof(IsSelected));
                }
            }
        }
    }
}
