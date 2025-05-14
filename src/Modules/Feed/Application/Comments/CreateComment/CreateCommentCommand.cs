using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.CreateComment;

public class CreateCommentCommand(Guid postId, string text) : CommandBase<CreateCommentResponse>, IUserRequest
{
    public Guid UserId { get; private set; } = default!;
    public PostId PostId { get; } = new(postId);
    public string Text { get; } = text;

    public bool IsAdmin { get; private set; }

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
