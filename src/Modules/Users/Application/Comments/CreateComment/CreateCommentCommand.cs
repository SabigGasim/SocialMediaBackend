using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;

namespace SocialMediaBackend.Modules.Users.Application.Comments.CreateComment;

public class CreateCommentCommand(Guid postId, string text) 
    : CommandBase<CreateCommentResponse>, IUserRequest<CreateCommentCommand>
{
    public Guid UserId { get; private set; } = default!;
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
        UserId = userId;
        return this;
    }
}
