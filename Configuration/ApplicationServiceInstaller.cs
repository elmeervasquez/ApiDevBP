using System.Reflection;
using ApiDevBP.Common;
using ApiDevBP.Configuration;

namespace planck.API.Configuration
{
    public class ApplicationServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(IMapperProfile));

            services.Configure<DbSettings>(configuration.GetSection(nameof(DbSettings)));

            // Register all transient services.
            RegisterInjectableServices(services);
        }

        void RegisterInjectableServices(IServiceCollection services)
        {
            var assemblies = typeof(InjectableAttribute).Assembly;

            var implementedClasses = assemblies.DefinedTypes.Where(t =>
            t.IsClass &&
            t.GetCustomAttribute(typeof(InjectableAttribute), true) != null
            );


            foreach (var implementedClass in implementedClasses)
            {
                // TODO: Check if we should throw exception if multiple interfaces are declarated. Could be a previous validation.
                var serviceInterface = assemblies.DefinedTypes.Where(t => IsInterfaceServiceForImplementation(t, implementedClass)).FirstOrDefault();

                if (serviceInterface != null)
                {
                    services.AddTransient(serviceInterface, implementedClass);
                }
            }

        }

        public void Configure<T>(IServiceCollection services, IConfiguration config) where T : class
        {
            services.Configure<T>(config);
        }

        bool IsInterfaceServiceForImplementation(TypeInfo interfaceType, TypeInfo implementation)
        {
            return interfaceType.IsAssignableFrom(implementation) && interfaceType.Name[1..].Equals(implementation.Name);
        }

    }
}
