using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Infrastructure.DependencyInjection;
using DReporting.Service.Common;

namespace DReporting.Service
{
    public class DIModuleService : IDIModule
    {
        private IDependencyResolver dependencyResolver;
        
        public DIModuleService(IDependencyResolver _dependencyResolver)
        {
            dependencyResolver = _dependencyResolver;
        }

        public void Load(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver.Register<IDynamicResourceService, DynamicResourceService>();
            
        }
    }
}