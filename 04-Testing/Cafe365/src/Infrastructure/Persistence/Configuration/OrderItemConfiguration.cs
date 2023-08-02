using Cafe365.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe365.Infrastructure.Persistence.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasConversion(x => x.Value,
                x => new OrderItemId(x))
        .ValueGeneratedNever();

        builder.OwnsOne(t => t.Price, MoneyConfiguration.BuildAction);

        builder.HasOne(t => t.Order)
            .WithMany(t => t.Items)
            .HasForeignKey(t => t.OrderId)
            .IsRequired();

        builder.HasOne(t => t.Product)
            .WithMany()
            .HasForeignKey(t => t.ProductId)
            .IsRequired();
    }
}
