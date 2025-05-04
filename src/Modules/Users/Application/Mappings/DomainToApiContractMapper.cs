using SocialMediaBackend.Modules.Users.Application.Comments.CreateComment;
using SocialMediaBackend.Modules.Users.Application.Comments.GetAllPostComments;
using SocialMediaBackend.Modules.Users.Application.Comments.GetAllReplies;
using SocialMediaBackend.Modules.Users.Application.Comments.GetComment;
using SocialMediaBackend.Modules.Users.Application.Comments.ReplyToComment;
using SocialMediaBackend.Modules.Users.Application.Posts.CreatePost;
using SocialMediaBackend.Modules.Users.Application.Posts.GetPost;
using SocialMediaBackend.Modules.Users.Application.Users.CreateUser;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;
using SocialMediaBackend.Modules.Users.Application.Users.GetAllUsers;
using SocialMediaBackend.Modules.Users.Application.Users.GetFullUserDetails;
using SocialMediaBackend.Modules.Users.Application.Users.GetUser;
using SocialMediaBackend.Modules.Users.Domain.Feed;
using SocialMediaBackend.Modules.Users.Domain.Feed.Comments;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;
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

    public static GetUserResponse MapToGetResponse(this Author user)
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
            new GetUserResponse(
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
