using AutoPuTTY.Core.Parameters;
using AutoPuTTY.Desktop.Data;
using JsonSubTypes;
using Newtonsoft.Json;

namespace AutoPuTTY.Desktop.Parameters
{
    internal class ConnectionParameterViewModelBase<T> : ObservableObject 
        where T : ConnectionParameterBase 
    {
        public ConnectionParameterViewModelBase(T source)
        {
            this.Source = source;
        }

        public T Source { get; private set; }

        public bool IsHidden
        {
            get
            {
                return Source.IsHidden;
            }
        }
    }
}