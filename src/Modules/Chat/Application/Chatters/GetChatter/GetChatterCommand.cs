using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;

public class GetChatterCommand(Guid chatterId) : CommandBase<GetChatterResponse>
{
    public ChatterId ChatterId { get; } = new(chatterId);
}
