using SocialMediaBackend.Modules.Feed.Application.Authors.GetAuthor;
using SocialMediaBackend.Modules.Feed.Application.Posts.GetAllPosts;
using SocialMediaBackend.Modules.Feed.Application.Posts.GetPost;
using SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Application.Mappings;

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
            new GetAuthorResponse(
                post.Author.AuthorId,
                post.Author.Username,
                post.Author.Nickname,
                post.Author.FollowersCount,
                post.Author.FollowingCount,
                post.Author.ProfilePictureUrl)
            );
    }
}
