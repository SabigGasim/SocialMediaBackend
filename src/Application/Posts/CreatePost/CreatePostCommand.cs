using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Contracts;

namespace SocialMediaBackend.Application.Posts.CreatePost;

public class CreatePostCommand(
    Guid userId,
    string text,
    IEnumerable<MediaRequest> mediaItems) : CommandBase<CreatePostResponse>
{
    public Guid UserId { get; } = userId;
    public string Text { get; } = text;
    public IEnumerable<MediaRequest> MediaItems { get; } = mediaItems;
}
