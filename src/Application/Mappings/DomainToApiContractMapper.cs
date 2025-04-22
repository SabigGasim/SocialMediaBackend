using SocialMediaBackend.Application.Comments.CreateComment;
using SocialMediaBackend.Application.Comments.GetAllPostComments;
using SocialMediaBackend.Application.Comments.GetAllReplies;
using SocialMediaBackend.Application.Comments.GetComment;
using SocialMediaBackend.Application.Comments.ReplyToComment;
using SocialMediaBackend.Application.Posts.CreatePost;
using SocialMediaBackend.Application.Posts.GetPost;
using SocialMediaBackend.Application.Users.CreateUser;
using SocialMediaBackend.Application.Users.Follows.FollowUser;
using SocialMediaBackend.Application.Users.GetAllUsers;
using SocialMediaBackend.Application.Users.GetUser;
using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Users.Follows;
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

    public static ReplyToCommentResponse MapToReplyResponse(this Comment comment)
    {
        return new ReplyToCommentResponse(comment.Id);
    }

    public static GetCommentResponse MapToGetResponse(this Comment comment)
    {
        return new GetCommentResponse(
            comment.Id, 
            comment.PostId, 
            comment.Text,
            comment.LikesCount,
            comment.RepliesCount,
            comment.User.MapToGetResponse());
    }

    public static GetReplyShortResponse MapToGetReplyResponse(this Comment comment)
    {
        return new GetReplyShortResponse(
            comment.Id,
            comment.Text,
            comment.LikesCount,
            comment.RepliesCount,
            comment.User.MapToGetResponse());
    }
    public static GetAllRepliesResponse MapToGetRepliesResponse(this IEnumerable<Comment> replies,
        Guid parentId, int page, int pageSize, int totalCount)
    {
        return new GetAllRepliesResponse(
            parentId,
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
}
