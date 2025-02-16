using FinBeat_Tech_Test__Backend_dev_.Net.ef;

namespace FinBeat_Tech_Test__Backend_dev_.Net.logger.DBLogger
{
    public class DBLogger : ILogger, IDisposable
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose() { }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId,
                    TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.DBLoggerDbSet.Add(new DBLoggerClass() { savedMesseges = formatter(state, exception) });
                    db.SaveChanges();
                    Console.WriteLine(formatter(state, exception));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }


}
