using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Queries;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetFullUserDetails;
public class GetFullUserDetailsQuery : QueryBase<GetFullUserDetailsResponse>,
    IUserRequest<GetFullUserDetailsQuery>
{
    public Guid UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }

    public GetFullUserDetailsQuery AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public GetFullUserDetailsQuery WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
