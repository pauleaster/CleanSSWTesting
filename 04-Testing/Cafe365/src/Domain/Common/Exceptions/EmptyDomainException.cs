namespace Cafe365.Domain.Common.Exceptions;

public class EmptyDomainException : DomainException
{
    public EmptyDomainException(string msg) : base(msg) { }
}
