namespace Cafe365.Melbourne.Domain.Common.Interfaces;

public interface IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    public string? CreatedBy { get; set; } // TODO: String as userId? (https://github.com/SSWConsulting/Cafe365.Melbourne/issues/76)
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; } // TODO: String as userId? (https://github.com/SSWConsulting/Cafe365.Melbourne/issues/76)
}