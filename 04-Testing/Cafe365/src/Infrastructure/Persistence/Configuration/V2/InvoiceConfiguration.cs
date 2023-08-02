//using Cafe365.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Cafe365.Infrastructure.Persistence.Configuration.V2;

//public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
//{
//    public void Configure(EntityTypeBuilder<Invoice> builder)
//    {
//        builder.ToTable("Invoices");

//        builder.HasKey(t => t.Id);
//        builder.Property(t => t.Id)
//            .HasConversion(x => x.Value,
//                x => new InvoiceId(x))
//            .ValueGeneratedOnAdd();

//        builder.OwnsOne(t => t.Total, MoneyConfiguration.BuildAction);
//    }
//}