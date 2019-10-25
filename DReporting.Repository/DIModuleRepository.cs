using Autofac;
using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Infrastructure.DependencyInjection;
using Baasic.Client.Modules.DynamicResource;
using DReporting.Repository.Common;

namespace DReporting.Repository
{
    public class DIModuleRepository : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DynamicResourceRepository>().As<IDynamicResourceRepository>();
        }
    }
}
