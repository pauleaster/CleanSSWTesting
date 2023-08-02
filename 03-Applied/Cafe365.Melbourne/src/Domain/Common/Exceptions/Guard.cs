using System.Runtime.CompilerServices;

namespace Cafe365.Melbourne.Domain.Common.Exceptions;

internal static class Guard
{
    internal static class Against
    {
        public static void Condition(bool condition, string message)
        {
            if (condition)
                throw new ConditionDomainException(message);
        }

        public static void Empty(string value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyDomainException($"{paramName} cannot be empty");
        }

        public static void ZeroOrNegative(decimal value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value <= 0)
                throw new ZeroOrNegativeDomainException($"{paramName} cannot be zero or negative");
        }

        public static void Null<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null) where T : class
        {
            if (value is null)
                throw new NullDomainException($"{paramName} cannot be null");
        }
    }
}

public abstract class DomainException : Exception
{
    public DomainException()
        : base()
    {
    }

    public DomainException(string message)
        : base(message)
    {
    }

    public DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

public class ConditionDomainException : DomainException
{
    public ConditionDomainException(string msg) : base(msg) { }
}

public class EmptyDomainException : DomainException
{
    public EmptyDomainException(string msg) : base(msg) { }
}

public class NullDomainException : DomainException
{
    public NullDomainException(string msg) : base(msg) { }
}

public class ZeroOrNegativeDomainException : DomainException
{
    public ZeroOrNegativeDomainException(string msg) : base(msg) { }
}
