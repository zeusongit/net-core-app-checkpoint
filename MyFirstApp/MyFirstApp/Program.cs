using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace MyFirstApp
{
    class Program
    {
        static public string DefaultConnectionString { get; } = @"Server=(localdb)\\mssqllocaldb;Database=SampleData";
        static IReadOnlyDictionary<string, string> DefaultConnectionStrings { get; } =
            new Dictionary<string, string>()
            {
                ["Profile:Username"] = Environment.UserName,
                ["Window:Width"] = "50",
                ["Window:Height"] = "5",
                [$"AppConfiguration:ConnectionString"] = DefaultConnectionString,
            };
        static public IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(DefaultConnectionStrings);
            configurationBuilder.AddJsonFile("appsettings.json",true,true);
            configurationBuilder.AddCommandLine(args);

            Configuration = configurationBuilder.Build();
            Console.SetWindowSize(Int32.Parse(Configuration["AppConfiguration:MainWindow:Width"]), Int32.Parse(Configuration["AppConfiguration:MainWindow:Height"]));
            Console.WriteLine($"Hello { Configuration.GetValue<string>("Profile:Username")}");

            Console.WriteLine($"Top { Configuration.GetValue<string>("AppConfiguration:MainWindow:Top")}");
            Console.WriteLine($"Hello { Configuration.GetValue<string>("firstname")}");
            Console.ReadKey();
        }
    }
}
