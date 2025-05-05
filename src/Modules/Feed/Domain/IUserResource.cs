using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Domain;

public interface IUserResource
{
    AuthorId AuthorId { get; }
    Author Author { get; }
}