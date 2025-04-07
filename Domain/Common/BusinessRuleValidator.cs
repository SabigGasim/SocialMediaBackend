using SocialMediaBackend.Domain.Common.Exceptions;

namespace SocialMediaBackend.Domain.Common;

public abstract class BusinessRuleValidator
{
    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }

    protected static async Task CheckRuleAsync(IBusinessRule rule)
    {
        if (await rule.IsBrokenAsync())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}
