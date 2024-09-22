using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>().WithMany()
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.Property(x => x.TotalPrice);

            builder.ComplexProperty(
                x => x.OrderName, nameBuilder => nameBuilder.Property(on => on.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(50).IsRequired());

            builder.ComplexProperty(x => x.ShippingAddress, addressbuilder =>
            {
                addressbuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                addressbuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                addressbuilder.Property(a => a.AddressLine).HasMaxLength(50).IsRequired();
                addressbuilder.Property(a => a.ZipCode).HasMaxLength(50).IsRequired();
                addressbuilder.Property(a => a.EmailAddress).HasMaxLength(50);
                addressbuilder.Property(a => a.Country).HasMaxLength(50);
                addressbuilder.Property(a => a.State).HasMaxLength(50);
            });

            builder.ComplexProperty(x => x.BillingAddress, addressbuilder =>
            {
                addressbuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                addressbuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                addressbuilder.Property(a => a.AddressLine).HasMaxLength(50).IsRequired();
                addressbuilder.Property(a => a.ZipCode).HasMaxLength(50).IsRequired();
                addressbuilder.Property(a => a.EmailAddress).HasMaxLength(50);
                addressbuilder.Property(a => a.Country).HasMaxLength(50);
                addressbuilder.Property(a => a.State).HasMaxLength(50);
            });

            builder.ComplexProperty(x => x.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardName).HasMaxLength(50);
                paymentBuilder.Property(p => p.CardNumber).HasMaxLength(24).IsRequired();
                paymentBuilder.Property(p => p.CVV).HasMaxLength(3);
                paymentBuilder.Property(p => p.Expiration).HasMaxLength(10);
                paymentBuilder.Property(p => p.PaymentMethod);
            });

            builder.Property(o => o.OrderStatus)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(orderStatus => orderStatus.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));
        }
    }
}
