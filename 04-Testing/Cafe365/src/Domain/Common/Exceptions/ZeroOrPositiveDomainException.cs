namespace Cafe365.Domain.Common.Exceptions;

public class ZeroOrPositiveDomainException : DomainException
{
    public ZeroOrPositiveDomainException(string msg) : base(msg) { }
}
