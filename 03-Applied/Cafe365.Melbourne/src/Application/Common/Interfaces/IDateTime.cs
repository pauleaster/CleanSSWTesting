namespace Cafe365.Melbourne.Application.Common.Interfaces;

public interface IDateTime
{
    // TODO: Talk to Gordon about this - System Clock (https://github.com/SSWConsulting/Cafe365.Melbourne/issues/77)
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}