using AutoPuTTY.Core;
using AutoPuTTY.Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Desktop.Parameters
{
    internal class StringConnectionParameterViewModel : ConnectionParameterViewModelBase<StringConnectionParameter>
    {
        private string _text; 

        public StringConnectionParameterViewModel(StringConnectionParameter source) : base(source)
        {
            Text = source.Value;
        }

        public string Text
        {
            get
            {
                return _text; 
            }
            set
            {
                ErrorInfo error;
                if (Source.TrySetValue(value, out error))
                {
                    _text = value;
                    RaisePropertyChanged(nameof(Text));
                }
            }
        }
    }
}
