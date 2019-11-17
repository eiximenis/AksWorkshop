using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebClient.Clients;

namespace WebClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<IApiClient, ApiClient>();
            /*
            // ESTA PARTE ES EL CODIGO ORIGINAL QUE NO PROCESA EL HEADER DE DEVSPACES
            services.AddHttpClient("api").ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(Configuration["ApiUrl"], UriKind.Absolute);
            })
            */
            services.AddHttpContextAccessor();
            services.AddTransient<DevspacesMessageHandler>();
            services.AddHttpClient("api").ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(Configuration["ApiUrl"], UriKind.Absolute);
            }).AddHttpMessageHandler<DevspacesMessageHandler>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
