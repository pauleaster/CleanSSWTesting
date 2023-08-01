using Cafe365.Melbourne.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe365.Melbourne.Infrastructure.Persistence.Configuration;
internal static class MoneyConfiguration
{
    internal static void BuildAction<T>(OwnedNavigationBuilder<T, Money> priceBuilder) where T : class
    {
        priceBuilder.Property(m => m.Currency).HasMaxLength(3);
        priceBuilder.Property(m => m.Amount).HasPrecision(18, 2);
    }
}
