using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.CreatePost;

public class CreatePostCommand(
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
