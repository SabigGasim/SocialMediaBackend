using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.DeleteAuthor;

public class DeleteAuthorCommand : InternalCommandBase
{
    [JsonConstructor]
    public DeleteAuthorCommand(Guid id, AuthorId authorId) : base(id)
    {
        AuthorId = authorId;
    }

    public AuthorId AuthorId { get; }
}
