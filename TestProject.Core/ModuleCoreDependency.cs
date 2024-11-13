using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TestProject.Core
{
    public static class ModuleCoreDependency
    {
        public static IServiceCollection AddCoreDependency(this IServiceCollection services)
        {

            // services.AddTransient<IT1Service, T1Service>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //Configration Of Automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //  services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;

        }
    }
}
