using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions options) : base(options)
        {
        }
    }
}
