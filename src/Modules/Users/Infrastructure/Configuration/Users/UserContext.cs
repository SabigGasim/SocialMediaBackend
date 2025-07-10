using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Users;

public class UserContext(IExecutionContextAccessor accessor) : IUserContext
{
    private readonly IExecutionContextAccessor _accessor = accessor;

    public UserId UserId => new UserId(_accessor.UserId);
}
