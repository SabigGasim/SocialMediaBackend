using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Contracts;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.AppSubscriptions.ActivateAppSubscription;

[method: System.Text.Json.Serialization.JsonConstructor]
[method: Newtonsoft.Json.JsonConstructor]
public sealed class ActivateAppSubscriptionCommand(
    Guid id,
    AuthorId authorId,
    AppSubscriptionTier subscriptionTier) : InternalCommandBase(id)
{
    public AuthorId AuthorId { get; } = authorId;
    public AppSubscriptionTier SubscriptionTier { get; } = subscriptionTier;
}
