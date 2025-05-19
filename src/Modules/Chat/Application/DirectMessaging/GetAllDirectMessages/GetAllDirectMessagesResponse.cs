using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

namespace SocialMediaBackend.Modules.Chat.Application.DirectMessaging.GetAllDirectMessages;

public record GetDirectMessageResponse(
    Guid MessageId,
    Guid ChatterId,
    string Text,
    DateTimeOffset SentAt,
    MessageStatus? Status);

public record GetAllDirectMessagesResponse(IEnumerable<GetDirectMessageResponse> Messages);
