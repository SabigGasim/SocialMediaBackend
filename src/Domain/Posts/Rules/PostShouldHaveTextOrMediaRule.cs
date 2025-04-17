using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Common.ValueObjects;

namespace SocialMediaBackend.Domain.Posts.Rules;

internal class PostShouldHaveTextOrMediaRule(string? text, IEnumerable<Media>? media = null) : IBusinessRule
{
    private readonly string? _text = text;
    private readonly IEnumerable<Media>? _media = media;

    public string Message { get; } = "A post should contain at least one media item or a text";

    public bool IsBroken() => string.IsNullOrWhiteSpace(_text) && _media?.Any() == false;

    public Task<bool> IsBrokenAsync()
    {
        throw new NotImplementedException();
    }
}
