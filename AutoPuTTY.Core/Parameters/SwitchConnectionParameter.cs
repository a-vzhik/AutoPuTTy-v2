namespace AutoPuTTY.Core.Parameters
{
    public class SwitchConnectionParameter : ConnectionParameterBase
    {
        public override string GetCommandLine()
        {
            return IsOn ? OnValue : OffValue;
        }

        protected override bool CanSetValue(string value, out ErrorInfo errorInfo)
        {
            errorInfo = null;
            var result = OnValue == value ||  OffValue == value;
            if (!result) {
                errorInfo = new ErrorInfo($"Значение должно быть {OnValue} или {OffValue}");
            }
            return result;
        }

        public bool IsOn { get; set; }

        public string OnValue { get; set; }

        public string OffValue { get; set; }
    }
}
