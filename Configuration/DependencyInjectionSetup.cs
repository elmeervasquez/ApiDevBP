using System.Reflection;

namespace ApiDevBP.Configuration
{
    /// <summary>
    /// Class DependencyInjectionSetup allows to separate all the injection and configuration services that implements IServiceInstaller.
    /// Credits: Milan Jovanovic. Microsoft MVP Architecture.
    /// </summary>
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection InstallServices(
            this IServiceCollection services,
            IConfiguration configuration,
            params Assembly[] assemblies)
        {
            var registrarServices = assemblies
                .SelectMany(a => a.DefinedTypes)
                .Where(IsAssignableToType<IServiceInstaller>)
                .Select(Activator.CreateInstance)
                .Cast<IServiceInstaller>();

            foreach (var registrarService in registrarServices)
            {
                registrarService.Install(services, configuration);
            }

            return services;
        }

        static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
            typeof(T).IsAssignableFrom(typeInfo) &&
            !typeInfo.IsInterface &&
            !typeInfo.IsAbstract;
    }
}
