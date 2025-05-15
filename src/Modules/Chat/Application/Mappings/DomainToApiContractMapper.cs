using SocialMediaBackend.Modules.Chat.Application.Chatters.GetAllChatters;
using SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Application.Mappings;

public static class DomainToApiContractMapper
{
    public static GetChatterResponse MapToGetResponse(this Chatter chatter)
    {
        return new GetChatterResponse(
            chatter.Id.Value,
            chatter.Username,
            chatter.Nickname,
            chatter.FollowersCount,
            chatter.FollowingCount,
            chatter.ProfilePicture.Url
            );
    }

    public static GetAllChattersResponse MapToResponse(this IEnumerable<Chatter> chatters, int pageNumber, int pageSize, int totalCount)
    {
        return new GetAllChattersResponse(
            pageNumber,
            pageSize,
            totalCount,
            chatters.Select(MapToGetResponse));
    }
}
