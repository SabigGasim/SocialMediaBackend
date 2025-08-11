using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;
using SocialMediaBackend.BuildingBlocks.Application.Auth;


namespace SocialMediaBackend.Modules.Feed.Application.Posts.CreatePost;

[HasPermission(Permissions.CreatePost)]
public sealed class CreatePostCommand(
    string text,
    IEnumerable<string> mediaItems) : CommandBase<CreatePostResponse>, IRequireAuthorization
{
    public string Text { get; } = text;
    public IEnumerable<string> MediaItems { get; } = mediaItems;
}
