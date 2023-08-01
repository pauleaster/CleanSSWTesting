using Cafe365.Melbourne.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe365.Melbourne.Infrastructure.Persistence.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasConversion(x => x.Value,
                x => new ProductId(x))
            .ValueGeneratedOnAdd();

        builder.OwnsOne(t => t.Price, MoneyConfiguration.BuildAction);
    }
}
