using AutoPuTTY.Core.Parameters;
using AutoPuTTY.Desktop.Input;
using System.Windows.Input;

namespace AutoPuTTY.Desktop.Parameters
{
    internal class PasswordConnectionParameterViewModel : StringConnectionParameterViewModel
    {
        private DelegateCommand _command;
        private string _explicitText; 

        public PasswordConnectionParameterViewModel(StringConnectionParameter source) : base(source)
        {
        }

        public string ExplicitText
        {
            get
            {
                return _explicitText;
            }
            set
            {
                if(_explicitText != value)
                {
                    _explicitText = value;
                    RaisePropertyChanged(nameof(ExplicitText));

                    if (IsPasswordVisible)
                    {
                        Text = ExplicitText;
                    }
                }
            }

        }

        public bool IsPasswordVisible { get; private set; }

        public ICommand TogglePasswordVisibilityCommand
        {
            get
            {
                if (_command == null)
                {
                    _command = new DelegateCommand(() =>
                    {
                        IsPasswordVisible = !IsPasswordVisible;
                        ExplicitText = IsPasswordVisible ? Text : "";

                        RaisePropertyChanged(nameof(IsPasswordVisible));
                    });
                }

                return _command;
            }
                
        } 
    }
}