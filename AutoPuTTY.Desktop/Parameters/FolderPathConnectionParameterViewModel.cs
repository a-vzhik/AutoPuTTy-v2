using AutoPuTTY.Core;
using AutoPuTTY.Core.Parameters;
using AutoPuTTY.Desktop.Input;
using System.Windows.Forms;
using System.Windows.Input;

namespace AutoPuTTY.Desktop.Parameters
{
    internal class FolderPathConnectionParameterViewModel : ConnectionParameterViewModelBase<FolderPathConnectionParameter>
    {
        private DelegateCommand _selectFolderCommand;
        private DelegateCommand _clearCommand;

        private string _text;

        public FolderPathConnectionParameterViewModel(FolderPathConnectionParameter source)
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

        public ICommand SelectFolderCommand
        {
            get
            {
                if (_selectFolderCommand == null)
                {
                    _selectFolderCommand = new DelegateCommand(() =>
                    {
                        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
                        {
                            Description = "Выберите папку",
                            SelectedPath = Text
                        };
                        DialogResult result = folderBrowserDialog.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            Text = folderBrowserDialog.SelectedPath;
                        }
                    });
                }

                return _selectFolderCommand; 
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
