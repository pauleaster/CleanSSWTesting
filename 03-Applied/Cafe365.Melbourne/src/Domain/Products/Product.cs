using Cafe365.Melbourne.Domain.Common.Base;
using Cafe365.Melbourne.Domain.Common.ValueObjects;

namespace Cafe365.Melbourne.Domain.Products;

public record ProductId(Guid Value);

public class Product : BaseEntity<ProductId>
{
    public string Sku { get; set; } = null!;
    public string Name { get; set; } = null!;
    public Money Price { get; set; }
    public int AvailableStock { get; set; }
    public bool IsStockedItem { get; set; }
}
