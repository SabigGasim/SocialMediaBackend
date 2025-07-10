using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;

public class GetChatterQuery(Guid chatterId) : QueryBase<GetChatterResponse>
{
    public ChatterId ChatterId { get; } = new(chatterId);
}
