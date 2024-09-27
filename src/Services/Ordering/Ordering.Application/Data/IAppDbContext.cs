using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Application.Data
{
    public interface IAppDbContext
    {
        DbSet<Order> Orders { get; }
        DbSet<Customer> Customers { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<Product> Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
