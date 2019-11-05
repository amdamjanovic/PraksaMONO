using System.IO;
using Microsoft.AspNetCore;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal;
using Microsoft.Extensions.Options;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                   .UseKestrel()
                   .ConfigureServices(services => services.AddAutofac())
                   .UseContentRoot(Directory.GetCurrentDirectory())
                   .UseIISIntegration()
                   .UseStartup<Startup>()
                   .Build();
            host.Run();

        }
    }
}
