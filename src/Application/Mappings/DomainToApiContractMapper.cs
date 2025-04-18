using SocialMediaBackend.Application.Posts.CreatePost;
using SocialMediaBackend.Application.Posts.GetPost;
using SocialMediaBackend.Application.Users.CreateUser;
using SocialMediaBackend.Application.Users.GetAllUsers;
using SocialMediaBackend.Application.Users.GetUser;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;

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

    public static CreatePostResponse MapToCreateResponse(this Post post) => new(post.Id);
    public static GetPostResponse MapToGetResponse(this Post post)
    {
        return new GetPostResponse(
            post.Id,
            post.Text,
            post.MediaItems.Select(x => x.Url),
            post.Created,
            post.LastModified,
            new GetUserResponse(post.User.Id, post.User.Username, post.User.Nickname, post.User.ProfilePicture.Url)
            );
    }

    public static CreateCommentResponse MapToCreateResponse(this Comment comment)
    {
        return new CreateCommentResponse(comment.Id);
    }
}
