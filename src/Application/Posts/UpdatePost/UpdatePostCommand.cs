using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Comments.DeleteComment;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Posts.UpdatePost;

public class UpdatePostCommand(Guid postId, string text) : CommandBase, IUserRequest<UpdatePostCommand>
{
    public PostId PostId { get; } = new(postId);
    public string Text { get; } = text;

    public UserId UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public UpdatePostCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public UpdatePostCommand WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }
}
