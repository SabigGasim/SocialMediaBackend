using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.AppSubscriptions.AppSubscriptionCanceled;

[method: System.Text.Json.Serialization.JsonConstructor]
[method: Newtonsoft.Json.JsonConstructor]
public sealed class CancelAppSubscriptionCommand(
    Guid id,
    AuthorId authorId) : InternalCommandBase(id)
{
    public AuthorId AuthorId { get; } = authorId;
}
