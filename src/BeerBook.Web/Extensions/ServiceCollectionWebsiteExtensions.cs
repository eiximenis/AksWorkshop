using BeerBook.Web.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBook.Web.Extensions
{
    static class ServiceCollectionWebsiteExtensions
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ICatalogClient, CatalogClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Urls:Catalog"], UriKind.Absolute));

            services.AddHttpClient<IOrderClient, OrderClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Urls:Order"], UriKind.Absolute));

            return services;
        }
    }
}
