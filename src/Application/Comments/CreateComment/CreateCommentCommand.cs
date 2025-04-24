using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Comments.CreateComment;

public class CreateCommentCommand(Guid postId, string text) 
    : CommandBase<CreateCommentResponse>, IUserRequest<CreateCommentCommand>
{
    public UserId UserId { get; private set; } = default!;
    public PostId PostId { get; } = new(postId);
    public string Text { get; } = text;

    public bool IsAdmin { get; private set; }

    public CreateCommentCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public CreateCommentCommand WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }
}
