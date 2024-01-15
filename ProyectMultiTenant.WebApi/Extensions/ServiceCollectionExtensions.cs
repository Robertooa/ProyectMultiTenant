using Microsoft.Extensions.DependencyInjection;
using ProyectMultiTenant.Domain.Tenant;

namespace ProyectMultiTenant.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMultitenancy(this IServiceCollection services)
        {
            services.AddScoped<TenantContext>();

            services.AddScoped<ITenantContext>(provider =>
                provider.GetRequiredService<TenantContext>());

            services.AddScoped<ITenantSetter>(provider =>
                provider.GetRequiredService<TenantContext>());

            return services;
        }
    }
}
