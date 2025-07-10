using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;
using SocialMediaBackend.BuildingBlocks.Application.Auth;


namespace SocialMediaBackend.Modules.Feed.Application.Posts.CreatePost;

[HasPermission(Permissions.CreatePost)]
public sealed class CreatePostCommand(
    string text,
    IEnumerable<string> mediaItems) : CommandBase<CreatePostResponse>, IUserRequest
{
    public string Text { get; } = text;
    public IEnumerable<string> MediaItems { get; } = mediaItems;

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
