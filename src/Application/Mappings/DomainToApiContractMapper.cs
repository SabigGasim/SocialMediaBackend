using FastEndpoints;
using SocialMediaBackend.Application.Users.CreateUser;
using SocialMediaBackend.Application.Users.GetAllUsers;
using SocialMediaBackend.Application.Users.GetUser;
using SocialMediaBackend.Domain.Users;
using System.Runtime.CompilerServices;

namespace SocialMediaBackend.Application.Mappings;

public static class DomainToApiContractMapper
{
    public static CreateUserResponse MapToCreateResponse(this User user)
    {
        return new CreateUserResponse(
            user.Id,
            user.Username,
            user.Nickname,
            user.DateOfBirth,
            user.ProfilePicture);
    }

    public static GetUserResponse MapToGetResponse(this User user)
    {
        return new GetUserResponse(
            user.Id,
            user.Username,
            user.Nickname,
            user.ProfilePicture
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

    public static CreatePostResponse MapToCreateResponse(this Post post) => new(post.Id);
    public static GetPostResponse MapToGetResponse(this Post post)
    {
        return new GetPostResponse(
            post.Id,
            post.UserId,
            post.User.Username,
            post.User.Nickname,
            post.User.ProfilePicture.Url,
            post.MediaItems.Select(x => x.Url),
            post.Created);
    }
}
