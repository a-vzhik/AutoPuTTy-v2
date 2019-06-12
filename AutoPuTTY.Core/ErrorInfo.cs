namespace AutoPuTTY.Core
{
    public class ErrorInfo
    {
        public ErrorInfo(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}