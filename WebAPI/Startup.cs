using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using WebApi;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("development.json", optional: true, reloadOnChange: true)
              //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
            this.Configuration = builder.Build();

           //Configuration = configuration;
        }

        public IConfigurationRoot Configuration { get; set; }
        public ILifetimeScope AutofacContainer { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Singleton objects are the same for every object and every request
            services.AddSingleton(Configuration.GetSection("Configuration").Get<Configuration>());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // create a container-builder and register dependencies
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new AutofacModule());

            // populate the Autofac container with services (service-descriptors added to `IServiceCollection`)
            containerBuilder.Populate(services);

            AutofacContainer = containerBuilder.Build();

            // this will be used as the service-provider for the application!
            return new AutofacServiceProvider(AutofacContainer);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        
    }
    
}
