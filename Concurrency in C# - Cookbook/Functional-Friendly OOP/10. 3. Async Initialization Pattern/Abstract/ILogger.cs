namespace AsyncInitializationPattern.Abstract
{
    public interface ILogger
    {
        void WriteLine(string format, params object[] arg);
    }
}