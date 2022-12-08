using ASL.Hrms.SharedKernel.Interfaces;
using Autofac;
using System.Collections.Generic;
using System.Reflection;
using Module = Autofac.Module;

namespace ASL.Hrms.SharedKernel
{
    public class DefaultSharedModule : Module
    {
        private bool _isDevelopment = false;
        private List<Assembly> _assemblies = new List<Assembly>();

        public DefaultSharedModule(bool isDevelopment, Assembly callingAssembly = null)
        {
            _isDevelopment = isDevelopment;
            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_isDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }
            RegisterCommonDependencies(builder);
        }

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventDispatcher>().As<IDomainEventDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(_assemblies.ToArray())
                .AsClosedTypesOf(typeof(IHandle<>));
        }

        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add development only services
        }

        private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add production only services
        }
    }
}
