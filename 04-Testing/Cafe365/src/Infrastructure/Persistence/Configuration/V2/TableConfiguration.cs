//using Cafe365.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Cafe365.Infrastructure.Persistence.Configuration.V2;

//public class TableConfiguration : IEntityTypeConfiguration<Table>

//{
//    public void Configure(EntityTypeBuilder<Table> builder)
//    {
//        builder.ToTable("Tables");

//        builder.HasKey(t => t.Id);
//        builder.Property(t => t.Id)
//            .HasConversion(x => x.Value,
//                x => new TableId(x))
//            .ValueGeneratedOnAdd();
//    }
//}
