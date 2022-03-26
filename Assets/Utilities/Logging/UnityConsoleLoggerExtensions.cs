namespace Osiris.Utilities.Logging
{
    public static class UnityConsoleLoggerExtensions
    {
        public static void Configure(this UnityConsoleLogger logger)
        {
            if (logger == null)
            {
                logger = new NullConsoleLogger();
            }
        }
    }
}
