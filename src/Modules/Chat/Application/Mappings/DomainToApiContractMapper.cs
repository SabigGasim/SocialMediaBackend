using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Chat.Application.Chatters.GetAllChatters;
using SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;
using SocialMediaBackend.Modules.Chat.Application.DirectMessaging.CreateDirectChat;
using SocialMediaBackend.Modules.Chat.Application.DirectMessaging.CreateDirectMessage;
using SocialMediaBackend.Modules.Chat.Application.DirectMessaging.SendDirectMessage;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

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

    public static CreateDirectChatResponse MapToResponse(this DirectChat chat)
    {
        return new CreateDirectChatResponse(chat.Id.Value);
    }

    public static SingleUserResponse<DirectMessageMessage, SendDirectMessageResponse> MapToResponse(
        this DirectMessage message,
        ChatterId recieverId,
        string method)
    {
        return new SingleUserResponse<DirectMessageMessage, SendDirectMessageResponse>
        {
            Identifier = recieverId.Value.ToString(),
            Message = new(message.Id.Value, message.ChatId.Value, message.Text, message.SentAt),
            Method = method,
            Payload = new(message.Id.Value, message.Status)
        };
    }
}
