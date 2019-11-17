using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBook.Shared
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseConfiguratedPathBase(this IApplicationBuilder app, IConfiguration configuration, ILoggerFactory loggerFactory = null)
        {
            var pathBase = configuration.GetBasePath();

            if (!string.IsNullOrEmpty(pathBase))
            {
                if (loggerFactory != null)
                {
                    loggerFactory.CreateLogger("init").LogDebug($"Using PATH BASE '{pathBase}'");
                }
                app.UsePathBase(pathBase);
            }

            return app;
        }
    }
}
