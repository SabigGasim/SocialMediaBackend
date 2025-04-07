namespace SocialMediaBackend.Domain.Common;

public abstract class Entity<TId> : BusinessRuleValidator
{
    public virtual TId Id { get; protected set; } = default!;
}
