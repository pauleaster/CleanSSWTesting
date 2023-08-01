using Cafe365.Melbourne.Domain.Common.Base;

namespace Cafe365.Melbourne.Domain.Orders;

public record OrderSubmittedEvent(Order Order) : DomainEvent;
