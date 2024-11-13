using Microsoft.Extensions.DependencyInjection;
using TestProject.Infrustrucure.Abstract;
using TestProject.Infrustrucure.Reposetories;

namespace TestProject.Infrustrucure
{
    public static class ModuleInfructructureDependency
    {
        public static IServiceCollection AddInfrustuctureDependencies(this IServiceCollection services)
        {

            services.AddTransient<IT1Reposatory, T1Reposetory>();
            services.AddTransient<IPeapleBusniseRepository, PeapleBusniseRepository>();
            services.AddTransient<IVendorRepository, VendorRepository>();
            services.AddTransient<IControlRepository, ControlRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasket_sRepository, Basket_sRepository>();
            services.AddScoped<IDynamicItemRepository, DynamicItemRepository>();
            //  services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;

        }
    }
}
