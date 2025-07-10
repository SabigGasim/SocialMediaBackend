using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;

namespace SocialMediaBackend.Api.Configuration;

public class ExecutionContextAccessor(IHttpContextAccessor accessor) : IExecutionContextAccessor
{
    private readonly IHttpContextAccessor _accessor = accessor;

    public Guid UserId => GetUserId();
    public bool IsAvailable => _accessor.HttpContext is not null;

    private Guid GetUserId()
    {
        if (!IsAvailable)
        {
            throw new NotFoundException(nameof(UserId));
        }

        var userIdClaim = _accessor
            .HttpContext!
            .User
            .Claims
            .FirstOrDefault(x => x.Type == "userid")?
            .Value;

        return Guid.TryParse(userIdClaim, out var userId)
            ? userId
            : throw new NotFoundException(nameof(UserId));
    }
}
