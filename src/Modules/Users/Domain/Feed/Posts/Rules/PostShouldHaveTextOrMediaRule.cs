using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;

namespace SocialMediaBackend.Modules.Users.Domain.Feed.Posts.Rules;

internal class PostShouldHaveTextOrMediaRule(string? text, IEnumerable<Media>? media = null) : IBusinessRule
{
    private readonly string? _text = text;
    private readonly IEnumerable<Media>? _media = media;

    public string Message { get; } = "A post should contain at least one media item or a text";

    public bool IsBroken() => string.IsNullOrWhiteSpace(_text) && !(_media?.Any() ?? false);

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
