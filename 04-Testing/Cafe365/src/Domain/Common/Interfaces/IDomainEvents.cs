using Cafe365.Domain.Common.Base;

namespace Cafe365.Domain.Common.Interfaces;

public interface IDomainEvents
{
    IReadOnlyList<DomainEvent> DomainEvents { get; }

    void AddDomainEvent(DomainEvent domainEvent);

    void RemoveDomainEvent(DomainEvent domainEvent);

    void ClearDomainEvents();
}
