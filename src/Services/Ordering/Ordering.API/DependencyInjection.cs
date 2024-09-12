namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseApiServices(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
