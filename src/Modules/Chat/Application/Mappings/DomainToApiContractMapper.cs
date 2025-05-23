using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Chat.Application.Chatters.GetAllChatters;
using SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectChat;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectMessage;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupMessage;
using SocialMediaBackend.Modules.Chat.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Application.Mappings;

public static class DomainToApiContractMapper
{
    public static GetChatterShortResponse MapToGetResponse(this Chatter chatter)
    {
        return new GetChatterShortResponse(
            chatter.Id.Value,
            chatter.Username,
            chatter.Nickname,
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
            ReceiverId = recieverId.Value.ToString(),
            Message = new(message.Id.Value, message.ChatId.Value, message.Text, message.SentAt),
            Method = method,
            Payload = new(message.Id.Value, message.Status)
        };
    }
    public static CreateGroupChatMessage MapToMessage(this GroupChat groupChat, IEnumerable<Chatter> members)
    {
        return new CreateGroupChatMessage(
            groupChat.Id.Value,
            groupChat.Name,
            members.Select(x => x.MapToGetResponse()));
    }

    public static MultipleUsersResponse<CreateGroupChatMessage, CreateGroupChatResponse> MapToCreateResponse(this GroupChat groupChat, ChatterId[] requestedMemberIds, Chatter[] members)
    {
        return new MultipleUsersResponse<CreateGroupChatMessage, CreateGroupChatResponse>
        {
            Method = ChatHubMethods.ReceiveGroupChatCreated,
            ReceiverId = members.MapToReceivers(),
            Message = groupChat.MapToMessage(members),
            Payload = new(
                groupChat.Id.Value,
                members.Length != requestedMemberIds.Length
                ? requestedMemberIds
                    .Except(members.Select(x => x.Id))
                    .Select(x => x.Value)
                : null)
        };
    }

    public static MultipleUsersResponse<CreateGroupMessageMessage, SendGroupMessageResponse> MapToCreateResponse(this GroupMessage message, GroupChat groupChat)
    {
        return new MultipleUsersResponse<CreateGroupMessageMessage, SendGroupMessageResponse>
        {
            Method = ChatHubMethods.ReceiveGroupMessage,
            Message = new(message.Id.Value, groupChat.Id.Value, message.SenderId.Value, message.Text, message.SentAt),
            Payload = new(groupChat.Id.Value),
            ReceiverId = groupChat.Members.Select(x => x.MemberId.Value.ToString())
        };
    }

    public static IEnumerable<string> MapToReceivers(this IEnumerable<Chatter> chatters)
    {
        return chatters.Select(x => x.Id.Value.ToString());
    }
}
