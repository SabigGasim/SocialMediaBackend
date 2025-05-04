namespace SocialMediaBackend.BuildingBlocks.Domain;

public abstract class Entity<TId>
{
    public virtual TId Id { get; protected set; } = default!;

    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }

    protected static async Task CheckRuleAsync(IBusinessRule rule, CancellationToken ct = default)
    {
        if (await rule.IsBrokenAsync(ct))
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}
