using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeerBook.Order.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BeerBook.Shared;
using BeerBook.Shared.Configuration;
using Microsoft.Extensions.Hosting;

namespace BeerBook.Order
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build()
                .MigrateDbContext<OrderContext>()
                .Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(wb =>
                {
                    wb.ConfigureAppConfiguration(cb => cb.AddFolder("/kv-data", "order"))
                      .UseStartup<Startup>();
                });
    }
}
