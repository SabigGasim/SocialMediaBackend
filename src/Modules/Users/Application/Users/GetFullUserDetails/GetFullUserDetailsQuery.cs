using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Users.Domain.Authorization;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetFullUserDetails;

[HasPermission(Permissions.GetFullUserDetails)]
public sealed class GetFullUserDetailsQuery : QueryBase<GetFullUserDetailsResponse>, IUserRequest
{
    public Guid UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public void WithAdminRole(bool isAdmin) => IsAdmin = isAdmin;
    public void WithUserId(Guid userId) => UserId = userId;
}
