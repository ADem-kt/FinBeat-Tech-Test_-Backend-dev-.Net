namespace FinBeat_Tech_Test__Backend_dev_.Net.logger.DBLogger
{
    public static class DBLoggerExtensions
    {
        public static ILoggingBuilder AddDB(this ILoggingBuilder builder)
        {
            builder.AddProvider(new DBLoggerProvider());
            return builder;
        }

    }
}
