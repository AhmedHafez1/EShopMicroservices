using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();
            await SeedAsync(context);
        }

        public static async Task SeedAsync(AppDbContext context)
        {
            await SeedCustomersAsync(context);
            await SeedProductsAsync(context);
            await SeedOrdersAndItemsAsync(context);
        }

        public static async Task SeedCustomersAsync(AppDbContext context)
        {
            if (!context.Customers.Any())
            {
                await context.Customers.AddRangeAsync(InitialData.Customers);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedProductsAsync(AppDbContext context)
        {
            if (!context.Products.Any())
            {
                await context.Products.AddRangeAsync(InitialData.Products);
                await context.SaveChangesAsync();
            }
        }
        public static async Task SeedOrdersAndItemsAsync(AppDbContext context)
        {
            if (!context.Orders.Any())
            {
                await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
                await context.SaveChangesAsync();
            }
        }

        public static bool HasOwnEntitiesChanged(this EntityEntry entry) => entry.References.Any(r =>
        r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned()
        && (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
