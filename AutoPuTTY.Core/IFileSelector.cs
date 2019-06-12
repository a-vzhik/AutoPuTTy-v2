namespace AutoPuTTY.Core
{
    public interface IFileSelector
    {
        string SelectFile(ConnectionDescription connection, string title, string filter);
    }
}