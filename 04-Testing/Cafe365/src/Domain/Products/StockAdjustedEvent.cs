using Cafe365.Domain.Common.Base;

namespace Cafe365.Domain.Products;

public record StockAdjustedEvent(Product Product) : DomainEvent;