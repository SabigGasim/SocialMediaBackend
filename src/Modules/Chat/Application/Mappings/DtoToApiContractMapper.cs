using SocialMediaBackend.Modules.Chat.Application.DirectMessaging.GetAllDirectMessages;
using SocialMediaBackend.Modules.Chat.Application.GroupMessaging.GetAllGroupMessages;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations;

namespace SocialMediaBackend.Modules.Chat.Application.Mappings;

internal static class DtoToApiContractMapper
{
    public static GetDirectMessageResponse MapToResponse(this DirectMessageDto message)
    {
        return new GetDirectMessageResponse(
            message.MessageId,
            message.SenderId, 
            message.Text, 
            message.SentAt, 
            message.Status);
    }

    public static GetAllDirectMessagesResponse MapToResponse(this IEnumerable<DirectMessageDto> messages)
    {
        return new GetAllDirectMessagesResponse(messages.Select(x => x.MapToResponse()));
    }

    public static GetGroupMessageResponse MapToResponse(this GroupMessageDto message)
    {
        return new GetGroupMessageResponse(
            message.MessageId,
            message.SenderId,
            message.Text,
            message.SentAt);
    }

    public static GetAllGroupMessagesResponse MapToResponse(this IEnumerable<GroupMessageDto> messages)
    {
        return new GetAllGroupMessagesResponse(messages.Select(x => x.MapToResponse()));
    }
}
