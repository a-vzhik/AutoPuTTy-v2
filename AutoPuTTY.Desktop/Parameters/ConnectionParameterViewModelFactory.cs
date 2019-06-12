using AutoPuTTY.Core;
using AutoPuTTY.Core.Parameters;
using System;

namespace AutoPuTTY.Desktop.Parameters
{
    public class ConnectionParameterViewModelFactory
    {
        public object Create(ConnectionParameterBase param)
        {
            if (param is FilePathConnectionParameter)
            {
                return new FilePathConnectionParameterViewModel((FilePathConnectionParameter)param);
            }

            if (param is FolderPathConnectionParameter)
            {
                return new FolderPathConnectionParameterViewModel((FolderPathConnectionParameter)param);
            }

            if (param is StringConnectionParameter)
            {
                if (param.Name == KnownParameters.Password)
                    return new PasswordConnectionParameterViewModel((StringConnectionParameter)param);
                return new StringConnectionParameterViewModel((StringConnectionParameter)param);
            }

            if (param is SwitchConnectionParameter)
            {
                return new SwitchConnectionParameterViewModel((SwitchConnectionParameter)param);
            }

            if (param is PredefinedConnectionParameter)
            {
                return new PredefinedConnectionParameterViewModel((PredefinedConnectionParameter)param);
            }

            throw new Exception("Unsupported parameter type");
           
        }
    }
}