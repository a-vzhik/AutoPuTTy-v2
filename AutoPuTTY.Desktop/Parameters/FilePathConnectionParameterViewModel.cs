using AutoPuTTY.Core;
using AutoPuTTY.Core.Parameters;
using AutoPuTTY.Desktop.Input;
using System.Windows.Forms;
using System.Windows.Input;

namespace AutoPuTTY.Desktop.Parameters
{
    internal class FilePathConnectionParameterViewModel : ConnectionParameterViewModelBase<FilePathConnectionParameter>
    {
        private DelegateCommand _selectFileCommand;
        private DelegateCommand _clearCommand;

        private string _text;

        public FilePathConnectionParameterViewModel(FilePathConnectionParameter source)
            : base(source)
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

                ViewModelCommandHelper.Invalidate(_clearCommand);
            }
        }

        public ICommand SelectFileCommand
        {
            get
            {
                if (_selectFileCommand == null)
                {
                    _selectFileCommand = new DelegateCommand(() =>
                    {
                        var browseFile = new OpenFileDialog
                        {
                            Title = $"{Source.DisplayName}",
                        };

                        if (browseFile.ShowDialog() == DialogResult.OK)
                        {
                            Text = browseFile.FileName;
                        }
                    });
                }

                return _selectFileCommand; 
            }
               
        }

        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new DelegateCommand(
                        () => Text = string.Empty, 
                        () => !string.IsNullOrEmpty(Text));
                }

                return _clearCommand;
            }
        }
    }
}
