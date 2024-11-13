using Microsoft.Extensions.DependencyInjection;
using TestProject.Service.Abstract;
using TestProject.Service.Implemention;

namespace TestProject.Service
{
    public static class ModuleServiceDependency
    {
        public static IServiceCollection AddServiceDependency(this IServiceCollection services)
        {

            services.AddTransient<IT1Service, T1Service>();
            services.AddTransient<IPeapleBusniseService, PeapleBusniseService>();
            services.AddTransient<IVendorService, VendorService>();
            services.AddTransient<IControlService, ControlService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasket_sService, Basket_sService>();
            services.AddScoped<IDynamicItemService, DynamicItemService>();
            //  services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;

        }
    }
}
