namespace SocialMediaBackend.BuildingBlocks.Domain;

public class BusinessRuleValidationException : DomainException
{
    public IBusinessRule BrokenRule { get; }

    public BusinessRuleValidationException(IBusinessRule brokenRule) : base(brokenRule.Message)
    {
        BrokenRule = brokenRule;
    }

    public override string ToString()
    {
        return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
    }
}
