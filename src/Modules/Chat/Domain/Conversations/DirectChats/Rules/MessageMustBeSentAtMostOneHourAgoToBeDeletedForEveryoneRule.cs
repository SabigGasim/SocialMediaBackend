using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats.Rules;

internal class MessageMustBeSentAtMostOneHourAgoToBeDeletedForEveryoneRule(DateTimeOffset sentAt) : IBusinessRule
{
    private readonly DateTimeOffset _sentAt = sentAt;

    public string Message { get; } = "A message must be sent at most one hour ago to be deleted for everyone";

    public bool IsBroken()
    {
        return DateTimeOffset.UtcNow.AddHours(-1) > _sentAt;
    }

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
