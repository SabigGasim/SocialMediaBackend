using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Users.GetFullUserDetails;
public class GetFullUserDetailsQuery : QueryBase<GetFullUserDetailsResponse>,
    IUserRequest<GetFullUserDetailsQuery>
{
    public UserId UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }

    public GetFullUserDetailsQuery AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public GetFullUserDetailsQuery WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }
}
