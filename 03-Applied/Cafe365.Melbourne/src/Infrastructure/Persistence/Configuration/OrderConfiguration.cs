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

    }
}
