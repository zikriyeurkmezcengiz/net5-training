using BookStoreWebApi.Services;

namespace BookStoreWebapi.Services
{
    public class LoggerFactory
    {
        public static ILoggerService GetLogger()
        {
            return new ConsoleLogger();
        }
    }
}