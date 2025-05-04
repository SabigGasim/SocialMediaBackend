using SocialMediaBackend.Modules.Users.Application.Posts.GetAllPosts;
using SocialMediaBackend.Modules.Users.Application.Posts.GetPost;
using SocialMediaBackend.Modules.Users.Application.Users.GetUser;
using SocialMediaBackend.Modules.Users.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Modules.Users.Application.Mappings;

internal static class DtoToApiContractMapper
{
    public static GetAllPostsResponse MapToResponse(
        this IEnumerable<PostDto> posts, 
        int pageNumber, 
        int pageSize,
        int totalCount)
    {
        return new GetAllPostsResponse(pageNumber, pageSize, totalCount, posts.Select(MapToResponse));
    }

    public static GetPostResponse MapToResponse(this PostDto post)
    {
        return new GetPostResponse(
            post.Id,
            post.Text,
            post.MediaUrls?.Split(','),
            post.CreatedAt,
            post.UpdatedAt,
            post.LikesCount,
            post.CommentsCount,
            new GetUserResponse(
                post.User.UserId,
                post.User.Username,
                post.User.Nickname,
                post.User.FollowersCount,
                post.User.FollowingCount,
                post.User.ProfilePictureUrl)
            );
    }
}
