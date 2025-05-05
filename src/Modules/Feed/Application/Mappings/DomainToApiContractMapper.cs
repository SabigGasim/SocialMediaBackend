using SocialMediaBackend.Modules.Feed.Application.Comments.CreateComment;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetAllPostComments;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetAllReplies;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetComment;
using SocialMediaBackend.Modules.Feed.Application.Comments.ReplyToComment;
using SocialMediaBackend.Modules.Feed.Application.Posts.CreatePost;
using SocialMediaBackend.Modules.Feed.Application.Posts.GetPost;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Application.Authors.GetAuthor;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Application.Authors.GetAllAuthors;

namespace SocialMediaBackend.Modules.Feed.Application.Mappings;

public static class DomainToApiContractMapper
{
    public static GetAuthorResponse MapToGetResponse(this Author user)
    {
        return new GetAuthorResponse(
            user.Id.Value,
            user.Username,
            user.Nickname,
            user.FollowersCount,
            user.FollowingCount,
            user.ProfilePicture.Url
            );
    }

    public static GetAllAuthorsResponse MapToResponse(this IEnumerable<Author> users, int pageNumber, int pageSize, int totalCount)
    {
        return new GetAllAuthorsResponse(
            pageNumber,
            pageSize,
            totalCount,
            users.Select(MapToGetResponse));
    }

    public static CreatePostResponse MapToCreateResponse(this Post post) => new(post.Id.Value);
    public static GetPostResponse MapToGetResponse(this Post post)
    {
        return new GetPostResponse(
            post.Id.Value,
            post.Text,
            post.MediaItems.Select(x => x.Url),
            post.Created,
            post.LastModified,
            post.LikesCount,
            post.CommentsCount,
            new GetAuthorResponse(
                post.Author.Id.Value,
                post.Author.Username,
                post.Author.Nickname,
                post.Author.FollowersCount,
                post.Author.FollowingCount,
                post.Author.ProfilePicture.Url)
            );
    }

    public static CreateCommentResponse MapToCreateResponse(this Comment comment)
    {
        return new CreateCommentResponse(comment.Id.Value);
    }

    public static ReplyToCommentResponse MapToReplyResponse(this Comment comment)
    {
        return new ReplyToCommentResponse(comment.Id.Value);
    }

    public static GetCommentResponse MapToGetResponse(this Comment comment)
    {
        return new GetCommentResponse(
            comment.Id.Value,
            comment.PostId.Value,
            comment.Text,
            comment.LikesCount,
            comment.RepliesCount,
            comment.Author.MapToGetResponse());
    }

    public static GetReplyShortResponse MapToGetReplyResponse(this Comment comment)
    {
        return new GetReplyShortResponse(
            comment.Id.Value,
            comment.Text,
            comment.LikesCount,
            comment.RepliesCount,
            comment.Author.MapToGetResponse());
    }
    public static GetAllRepliesResponse MapToGetRepliesResponse(this IEnumerable<Comment> replies,
        CommentId parentId, int page, int pageSize, int totalCount)
    {
        return new GetAllRepliesResponse(
            parentId.Value,
            page,
            pageSize,
            totalCount,
            replies.Select(MapToGetReplyResponse)
            );
    }


    public static GetAllPostCommentsResponse MapToResponse(
        this IEnumerable<Comment> comments,
        int page,
        int pageSize,
        int totalCount)
    {
        return new(page, pageSize, totalCount, comments.Select(MapToGetResponse));
    }
}
