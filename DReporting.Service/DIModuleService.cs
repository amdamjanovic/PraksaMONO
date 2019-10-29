using Autofac;
using DReporting.Model;
using DReporting.Service.Common;
using Microsoft.Extensions.Logging;

namespace DReporting.Service
{
    public class DIModuleService : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DynamicResourceService>().As<IDynamicResourceService<ReportModel>>();
        }
    }
}