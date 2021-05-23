using System;

namespace BookStoreWebApi.Services
{
    public class ConsoleLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("Console Logger - " + message);
        }
    }
}