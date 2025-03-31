namespace SocialMediaBackend.Domain.Common;

public abstract class AuditableEntity<TId> : Entity<TId>
{
    public required DateTimeOffset Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
