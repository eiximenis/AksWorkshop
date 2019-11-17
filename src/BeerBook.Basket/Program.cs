using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BeerBook.Shared.Configuration;
using Microsoft.Extensions.Hosting;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace BeerBook.Basket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var startupConfig = BuildStartupConfiguration();

            CreateHostBuilder(args, startupConfig).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration startupConf)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(wb =>
                 { 
                    wb.ConfigureAppConfiguration(cb => cb.AddFolder("/kv-data", "basket"))
                      .ConfigureKestrel(options =>
                      {
                          var ports = GetDefinedPorts(startupConf);
                          options.Listen(IPAddress.Any, ports.httpPort, listenOptions =>
                          {
                              listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                          });
                          options.Listen(IPAddress.Any, ports.grpcPort, listenOptions =>
                          {
                              listenOptions.Protocols = HttpProtocols.Http2;
                          });
                      })
                      .UseStartup<Startup>();
                });
        }

        private static IConfiguration BuildStartupConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        private static (int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
        {
            var port = config.GetValue("PORT", 80);
            var grpcPort = config.GetValue("GRPC_PORT", port + 1);
            return (port, grpcPort);
        }
    }
}
