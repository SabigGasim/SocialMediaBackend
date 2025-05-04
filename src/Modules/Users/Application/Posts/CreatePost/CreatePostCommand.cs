using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Application.Posts.CreatePost;

public class CreatePostCommand(
    string text,
    IEnumerable<MediaRequest> mediaItems) : CommandBase<CreatePostResponse>, IUserRequest<CreatePostCommand>
{
    public string Text { get; } = text;
    public IEnumerable<MediaRequest> MediaItems { get; } = mediaItems;

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
