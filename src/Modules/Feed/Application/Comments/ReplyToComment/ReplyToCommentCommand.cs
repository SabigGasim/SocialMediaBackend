using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.ReplyToComment;

[HasPermission(Permissions.ReplyToComment)]
public sealed class ReplyToCommentCommand(Guid parentId, string text): CommandBase<ReplyToCommentResponse>, IUserRequest
{
    public CommentId ParentId { get; } = new(parentId);
    public string Text { get; } = text;
    public Guid UserId { get; private set; } = default!;

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
