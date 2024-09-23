using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Interceptors;


namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("Default");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(new AuditableEntitiesInterceptor());
            });

            return services;
        }
    }
}
