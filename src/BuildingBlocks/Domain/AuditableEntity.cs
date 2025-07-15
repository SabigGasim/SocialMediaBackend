namespace SocialMediaBackend.BuildingBlocks.Domain;

public abstract class AuditableEntity<TId> : Entity<TId>
{
    protected AuditableEntity()
    {
        var now = DateTimeOffset.UtcNow;
        Created = now;
        CreatedBy = "System";
        LastModified = now;
        LastModifiedBy = "System";
    }

    public DateTimeOffset Created { get; init; }
    public string? CreatedBy { get; init; }
    public DateTimeOffset LastModified { get; protected set; }
    public string? LastModifiedBy { get; protected set; }
}
