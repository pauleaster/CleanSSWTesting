using Cafe365.Domain.Common.Base;
using Cafe365.Domain.Common.Exceptions;
using Cafe365.Domain.Common.ValueObjects;

namespace Cafe365.Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    public string Sku { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public Money Price { get; private set; } = null!;

    public bool IsStockedItem { get; private set; }
    public int AvailableStock { get; private set; }

    private Product() { }

    public static Product Create(string sku, string name, Money price, int availableStock)
    {
        var product = new Product()
        {
            Id = new ProductId(Guid.NewGuid()),
            Sku = sku,
            Name = name,
            Price = price,
            IsStockedItem = true,
            AvailableStock = availableStock
        };

        return product;
    }

    public void RemoveStock(int quantity)
    {
        if (!IsStockedItem)
            return;

        Guard.Against.ZeroOrNegative(quantity);

        AvailableStock -= quantity;

        if (AvailableStock < 0)
            throw new InvalidOperationException("Not enough stock submit the order");

        AddDomainEvent(new StockAdjustedEvent(this));
    }
}

public record ProductId(Guid Value);

