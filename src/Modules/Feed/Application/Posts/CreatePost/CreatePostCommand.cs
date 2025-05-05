using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.CreatePost;

public class CreatePostCommand(
    string text,
    IEnumerable<string> mediaItems) : CommandBase<CreatePostResponse>, IUserRequest<CreatePostCommand>
{
    public string Text { get; } = text;
    public IEnumerable<string> MediaItems { get; } = mediaItems;

    public Guid UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public CreatePostCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public CreatePostCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
