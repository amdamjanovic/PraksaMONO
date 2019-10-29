using Autofac;
using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Infrastructure.DependencyInjection;
using Baasic.Client.Modules.DynamicResource;
using DReporting.Model;
using DReporting.Repository.Common;
using System.Reflection;

namespace DReporting.Repository
{
    public class DIModuleRepository : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DynamicResourceRepository>().As<IDynamicResourceRepository<ReportModel>>();
        }
    }
}
