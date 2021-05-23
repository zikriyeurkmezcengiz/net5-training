using System;

namespace BookStoreWebApi.Services
{
    public class DBLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("DB Logger - " + message);
        }
    }
}