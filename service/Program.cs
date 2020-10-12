using System;
using Serilog;

namespace EventSourcingCQRS
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            Log.Information("Starting Service");
        }
    }
}
