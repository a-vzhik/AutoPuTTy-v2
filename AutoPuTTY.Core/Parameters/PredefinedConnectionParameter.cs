using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPuTTY.Core.Parameters
{
    public class PredefinedConnectionParameter : ConnectionParameterBase
    {
        public PredefinedConnectionParameter()
        {
            Delimiter = " ";
            AllowedValues = new List<string>();
        }

        public override string GetCommandLine()
        {
            if (string.IsNullOrEmpty(Value))
            {
                return string.Empty;
            }

            return $"{CmdLineName}{Delimiter}{Value}";
        }

        public string CmdLineName { get; set; }

        public string Delimiter { get; set; }

        public IList<string> AllowedValues { get; set; }

        protected override bool CanSetValue(string value, out ErrorInfo errorInfo)
        {
            errorInfo = null;
            var isAllowed = string.IsNullOrEmpty(value) || AllowedValues.Contains(value);
            if (!isAllowed) {
                errorInfo = new ErrorInfo($"Значение {value} не разрещено для параметра {DisplayName}");
            }
            return isAllowed;
        }
    }
}
