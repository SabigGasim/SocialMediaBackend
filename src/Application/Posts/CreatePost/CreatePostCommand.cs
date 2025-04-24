using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Contracts;

namespace SocialMediaBackend.Application.Posts.CreatePost;

public class CreatePostCommand(
    string text,
    IEnumerable<MediaRequest> mediaItems) : CommandBase<CreatePostResponse>, IUserRequest<CreatePostCommand>
{
    public string Text { get; } = text;
    public IEnumerable<MediaRequest> MediaItems { get; } = mediaItems;

    public Guid UserId { get; private set; }
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
