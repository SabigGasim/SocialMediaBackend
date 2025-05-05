using SocialMediaBackend.Modules.Users.Application.Users.CreateUser;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;
using SocialMediaBackend.Modules.Users.Application.Users.GetAllUsers;
using SocialMediaBackend.Modules.Users.Application.Users.GetFullUserDetails;
using SocialMediaBackend.Modules.Users.Application.Users.GetUser;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;

namespace SocialMediaBackend.Modules.Users.Application.Mappings;

public static class DomainToApiContractMapper
{
    public static CreateUserResponse MapToCreateResponse(this User user)
    {
        return new CreateUserResponse(
            user.Id.Value,
            user.Username,
            user.Nickname,
            user.DateOfBirth,
            user.ProfilePicture);
    }

    public static GetUserResponse MapToGetResponse(this User user)
    {
        return new GetUserResponse(
            user.Id.Value,
            user.Username,
            user.Nickname,
            user.FollowersCount,
            user.FollowingCount,
            user.ProfilePicture.Url
            );
    }

    public static GetAllUsersResponse MapToResponse(this IEnumerable<User> users, int pageNumber, int pageSize, int totalCount)
    {
        return new GetAllUsersResponse(
            pageNumber, 
            pageSize, 
            totalCount, 
            users.Select(MapToGetResponse));
    }

    public static FollowUserResponse MapToFollowResponse(this Follow follow)
    {
        return new FollowUserResponse(follow.Status);
    }

    public static GetFullUserDetailsResponse MapToFullUserResponse(this User user)
    {
        return new GetFullUserDetailsResponse(
            user.Id.Value,
            user.Username,
            user.Nickname,
            user.FollowersCount,
            user.FollowingCount,
            user.DateOfBirth,
            user.ProfilePicture.Url,
            user.ProfileIsPublic);
    }
}
