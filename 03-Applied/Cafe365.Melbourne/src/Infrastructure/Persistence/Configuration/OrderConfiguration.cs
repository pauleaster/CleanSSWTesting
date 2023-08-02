using Cafe365.Melbourne.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe365.Melbourne.Infrastructure.Persistence.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasConversion(x => x.Value,
                x => new OrderId(x))
            .ValueGeneratedOnAdd();

        builder.HasOne(t => t.Customer)
            .WithMany(t => t.Orders)
            .HasForeignKey(t => t.CustomerId)
            .IsRequired();

        builder.OwnsOne(t => t.PaidTotal, MoneyConfiguration.BuildAction);

        // NOTE: Line items can be exposed in two ways:
        // 1. Define a shadow field that tells EF to use the backing field to access the collection
        // 2. With a read only navigation property on the Order object
        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey(li => li.OrderId)
            .IsRequired();
    }
}
