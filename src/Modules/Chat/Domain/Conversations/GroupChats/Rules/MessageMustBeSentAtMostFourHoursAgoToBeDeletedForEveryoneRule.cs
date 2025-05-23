using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;

internal class MessageMustBeSentAtMostFourHoursAgoToBeDeletedForEveryoneRule(DateTimeOffset sentAt) : IBusinessRule
{
    private readonly DateTimeOffset _sentAt = sentAt;

    public string Message { get; } = "A message must be sent at most four hour ago to be deleted for everyone";

    public bool IsBroken()
    {
        return DateTimeOffset.UtcNow.AddHours(-4) > _sentAt;
    }

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
