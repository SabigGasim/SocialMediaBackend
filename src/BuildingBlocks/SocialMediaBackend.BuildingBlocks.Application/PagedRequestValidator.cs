using FastEndpoints;
using FluentValidation;

namespace SocialMediaBackend.BuildingBlocks.Application;

public abstract class PagedRequestValidator<TPagedRequest> : Validator<TPagedRequest>
    where TPagedRequest : PagedRequest
{
    public PagedRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 10);
    }
}
