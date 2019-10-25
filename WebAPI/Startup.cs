using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using DReporting.Model;
using DReporting.Service.Common;
using DReporting.Service;
using DReporting.Repository.Common;
using Baasic.Client.Modules.DynamicResource;
using DReporting.WebAPI;
using System.Reflection;
using Baasic.Client.Common.Infrastructure.DependencyInjection;
using DReporting.Repository;
using System.IO;
using System.Linq;
using Baasic.Client.AutoFac;
using Microsoft.Extensions.Logging;

namespace WebAPI
{
    #region Startup Development
    // Fallback Startup class
    // Selected if the environment doesn't match a Startup{EnvironmentName} class
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public ILifetimeScope AutofacContainer { get; set; }
        public ReportModel ReportModel { get; }
        

        public Startup(IConfiguration _config, IHostingEnvironment env)
        {
            Configuration = _config;
            ReportModel reportModel = new ReportModel();

            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();
            ConfigurationBinder.Bind(Configuration, reportModel);
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            
            services.AddTransient<IDynamicResourceService, DynamicResourceService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddControllersAsServices();
            
            AutofacContainer = ConfigureDIAutofac();
            
            return new AutofacServiceProvider(AutofacContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // The Configure method is used to specify how the app responds to HTTP requests
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

        public IContainer ConfigureDIAutofac()
        {
            var containerBuilder = new ContainerBuilder();
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var assemblies = Directory.GetFiles(path, "DReporting.*.dll").Select(Assembly.LoadFrom).ToArray();

            containerBuilder.RegisterModule(new AutofacModule());
            
            containerBuilder.RegisterAssemblyModules(assemblies);

            var autofacSettings = new AutoFacSettings(containerBuilder);
            var resolver = new AutofacDependencyResolver(autofacSettings);
            resolver.Initialize();

            var baasicDIModules = assemblies.SelectMany(x => x.GetTypes())
                .Where(x => typeof(IDIModule).IsAssignableFrom(x)
                && !x.IsInterface
                && !x.IsAbstract
                && x.GetConstructor(Type.EmptyTypes) != null)
                .Select(x => Activator.CreateInstance(x) as IDIModule)
                .ToArray();

            foreach (var instance in baasicDIModules)
            {
                instance.Load(resolver);
            }
            autofacSettings.Build();

            return autofacSettings.Container;
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

