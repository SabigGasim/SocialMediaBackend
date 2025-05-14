using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetFullUserDetails;
public class GetFullUserDetailsQuery : QueryBase<GetFullUserDetailsResponse>, IUserRequest
{
    public Guid UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
