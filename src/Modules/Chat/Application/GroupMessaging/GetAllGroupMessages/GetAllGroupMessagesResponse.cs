namespace SocialMediaBackend.Modules.Chat.Application.GroupMessaging.GetAllGroupMessages;

public record GetGroupMessageResponse(
    Guid MessageId,
    Guid ChatterId,
    string Text,
    DateTimeOffset SentAt);

public record GetAllGroupMessagesResponse(IEnumerable<GetGroupMessageResponse> Messages);