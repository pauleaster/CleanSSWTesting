using Cafe365.Domain.Common.Base;
using Cafe365.Domain.Customers;

namespace Cafe365.Domain.Orders;

public record OrderSubmittedEvent(Order Order, Customer Customer) : DomainEvent;
