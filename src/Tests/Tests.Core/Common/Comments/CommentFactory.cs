using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;

namespace Tests.Core.Common.Comments;

public static class CommentFactory
{
    public static Comment Create(string text = "text")
    {
        return Comment.Create(PostId.New(), UserId.New(), text, null);
    }

    public static Comment CreateReply(Comment comment, string text = "text")
    {
        return Comment.Create(comment.PostId, comment.UserId, text, comment.Id);
    }
}
