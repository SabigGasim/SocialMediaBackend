namespace SocialMediaBackend.BuildingBlocks.Domain;

public abstract class AuditableEntity<TId> : Entity<TId>
{
    protected AuditableEntity()
    {
        Created = DateTimeOffset.UtcNow;
        CreatedBy = "System";
        LastModified = DateTimeOffset.UtcNow;
        LastModifiedBy = "System";
    }

    public DateTimeOffset Created { get; init; }
    public string? CreatedBy { get; init; }
    public DateTimeOffset LastModified { get; protected set; }
    public string? LastModifiedBy { get; protected set; }
}
