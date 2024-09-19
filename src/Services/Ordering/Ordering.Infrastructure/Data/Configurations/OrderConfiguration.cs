using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId));


            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x=> x.CustomerId)
                .IsRequired();

            builder.HasMany(x=> x.OrderItems)
                .WithOne()
                .HasForeignKey(x=> x.OrderId);

            builder.ComplexProperty(x=> x.OrderName, nameBuilder =>
            {
                nameBuilder.Property(y=> y.Value)
                .HasColumnName(nameof(OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });

            builder.ComplexProperty(x=> x.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(y=> y.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(y=> y.LastName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(y=> y.EmailAddress)
                .HasMaxLength(50);

                addressBuilder.Property(y=> y.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                addressBuilder.Property(y=> y.Country)
                .HasMaxLength(50);

                addressBuilder.Property(y=> y.State)
                .HasMaxLength(50);

                addressBuilder.Property(y=> y.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

            builder.ComplexProperty(x=> x.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(y=> y.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(y=> y.LastName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(y=> y.EmailAddress)
                .HasMaxLength(50);

                addressBuilder.Property(y=> y.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                addressBuilder.Property(y=> y.Country)
                .HasMaxLength(50);

                addressBuilder.Property(y=> y.State)
                .HasMaxLength(50);

                addressBuilder.Property(y=> y.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

            builder.ComplexProperty(x=> x.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(y=> y.CardName)
                .HasMaxLength(50);

                paymentBuilder.Property(y=> y.CardNumber)
                .HasMaxLength(24)
                .IsRequired();

                paymentBuilder.Property(y=> y.Expiration)
                .HasMaxLength(10);

                paymentBuilder.Property(y=> y.CVV)
                .HasMaxLength(3);

                paymentBuilder.Property(y=> y.PaymentMethod);
            });

            builder.Property(x=> x.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(x=> x.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(x=>x.TotalPrice);
        }
    }
}
