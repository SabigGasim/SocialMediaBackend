using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Domain.Feed.Posts;
using SocialMediaBackend.Domain.Users;

namespace Tests.Core.Common.Comments;

public static class CommentFactory
{
    public static Comment Create(PostId? postId = null, UserId? userId = null, string text = "text")
    {
        return Comment.Create(postId ?? PostId.New(), userId ?? UserId.New(), text, null);
    }

    public static Comment CreateReply(Comment comment, string text = "text")
    {
        return Comment.Create(comment.PostId, comment.UserId, text, comment.Id);
    }
}
