using AutoPuTTY.Core;
using AutoPuTTY.Core.Parameters;
using AutoPuTTY.Desktop.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Desktop
{
    class ProfileSettingsViewModel
    {
        public ProfileSettingsViewModel(ConnectionDescription profile)
        {
            Profile = profile;

            FilePathParameter = new FilePathConnectionParameter()
            {
                Name = "filePath",
                DisplayName = "Путь к exe",
                Value = profile.AppPath
            };

            AppPathParameterViewModel = new FilePathConnectionParameterViewModel(FilePathParameter);
            AppPathParameterViewModel.PropertyChanged += FilePathParameterViewModel_PropertyChanged;

            var outputFolder = profile.Parameters.FirstOrDefault(p => p.Name == "OutputFolder") as FolderPathConnectionParameter;
            if(outputFolder !=  null)
            {
                OutputFolderParameterViewModel = new FolderPathConnectionParameterViewModel(outputFolder);
            }

        }

        private void FilePathParameterViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FilePathConnectionParameterViewModel.Text))
            {
                Profile.AppPath = FilePathParameter.Value;
            }
        }

        public ConnectionDescription Profile
        {
            get;
            private set;
        }

        public FilePathConnectionParameter FilePathParameter
        {
            get; private set;
        }

        public FilePathConnectionParameterViewModel AppPathParameterViewModel { get; }

        public string Title => Profile?.ConnectionTypeName;

        public FolderPathConnectionParameterViewModel OutputFolderParameterViewModel { get; }

        public override string ToString()
        {
            return Title;
        }
    }
}
