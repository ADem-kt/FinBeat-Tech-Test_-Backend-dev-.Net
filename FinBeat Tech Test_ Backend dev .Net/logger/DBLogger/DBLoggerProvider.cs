namespace FinBeat_Tech_Test__Backend_dev_.Net.logger.DBLogger
{
    public class DBLoggerProvider : ILoggerProvider
    {

        public ILogger CreateLogger(string categoryName)
        {
            return new DBLogger();
        }

        public void Dispose() { }
    }
}
