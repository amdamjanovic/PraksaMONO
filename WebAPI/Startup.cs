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
using DReporting.Model;

namespace WebAPI
{
    #region Startup Development
    // Fallback Startup class
    // Selected if the environment doesn't match a Startup{EnvironmentName} class
    public class Startup
    {
        public Startup(IConfiguration _config, IHostingEnvironment env)
        {
            ReportModel reportModel = new ReportModel();

            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();
            ConfigurationBinder.Bind(Configuration, reportModel);
            /*
            var report = new ReportModel();
            _config.GetSection("ConnectionDev").Bind(report);
            ReportModel = report;*/
        }
        
        public IConfiguration Configuration { get; set; }
        public ILifetimeScope AutofacContainer { get; set; }
        public ReportModel ReportModel { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var config = new ReportModel();
            Configuration.Bind("ConnectionDev", config);
            services.AddSingleton(config);

            //services.Configure<ReportModel>(options => Configuration.GetSection("ConnectionDev").Bind(options));
            
            //Singleton objects are the same for every object and every request
            //services.AddSingleton(Configuration.GetSection("ConnectionDev").Get<ReportModel>());

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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
    #endregion

    #region Startup Production 
    //STARTUP PRODUCTION
    public class StartupProduction
    {
        public StartupProduction(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }
        public ILifetimeScope AutofacContainer { get; set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Singleton objects are the same for every object and every request
            services.AddSingleton(Configuration.GetSection("ConnectionProd").Get<ReportModel>());

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsProduction() || env.IsStaging())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
    #endregion

    #region Startup Staging
    //STARTUP STAGING
    public class StartupStaging
    {
        public StartupStaging(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.Staging.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }
        public ILifetimeScope AutofacContainer { get; set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Singleton objects are the same for every object and every request
            services.AddSingleton(Configuration.GetSection("ConnectionStag").Get<ReportModel>());

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsStaging() || env.IsProduction())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
    #endregion

}

