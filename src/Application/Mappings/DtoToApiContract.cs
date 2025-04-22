using SocialMediaBackend.Application.Posts.GetAllPosts;
using SocialMediaBackend.Application.Posts.GetPost;
using SocialMediaBackend.Application.Users.GetUser;
using SocialMediaBackend.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Application.Mappings;

internal static class DtoToApiContractMappter
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
