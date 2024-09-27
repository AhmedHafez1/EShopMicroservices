using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infrastructure.Interceptors;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntitiesInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            });

            services.AddScoped<IAppDbContext, AppDbContext>();

            return services;
        }
    }
}
