using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Baasic.Client.AutoFac;
using Baasic.Client.Common;
using Baasic.Client.Common.Configuration;
using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Configuration;
using Baasic.Client.Modules.DynamicResource;
using DReporting.Model;
using DReporting.Repository;
using DReporting.Repository.Common;
using DReporting.Service;
using DReporting.Service.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public ILifetimeScope AutofacContainer { get; private set; }
        public ReportModel ReportModel { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            ReportModel reportModel = new ReportModel();

            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();
            
            Configuration = builder.Build();
            ConfigurationBinder.Bind(Configuration, reportModel);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // IServiceProvider is the component that resolves and creates the dependencies out of a IServiceCollection
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddControllersAsServices();
            
            services.AddScoped<IDynamicResourceService<ReportModel>, DynamicResourceService>();
            services.AddScoped<IDynamicResourceRepository<ReportModel>, DynamicResourceRepository>();
           // services.AddScoped<IDynamicResourceClient<ReportModel>, DynamicResourceClient<ReportModel>>();
           // services.AddScoped<IClientConfiguration, ClientConfiguration>();
            
            AutofacContainer = ConfigureDIAutofac(services);

            return new AutofacServiceProvider(AutofacContainer);
        }
        
        public IContainer ConfigureDIAutofac(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var assemblies = Directory.GetFiles(path, "DReporting.*.dll").Select(Assembly.LoadFrom).ToArray();

            containerBuilder.Populate(services);
            containerBuilder.RegisterModule(new AutofacModule());
            containerBuilder.RegisterAssemblyModules(assemblies);
            
            containerBuilder.RegisterType<DynamicResourceRepository>().UsingConstructor(typeof(IDynamicResourceClient<ReportModel>));
            containerBuilder.RegisterType<DynamicResourceClient<ReportModel>>().As<DynamicResourceClient<ReportModel>>();
            containerBuilder.RegisterType<ClientConfiguration>().As<ClientConfiguration>();
            
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
}

