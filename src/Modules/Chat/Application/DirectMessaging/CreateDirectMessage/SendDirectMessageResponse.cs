using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

namespace SocialMediaBackend.Modules.Chat.Application.DirectMessaging.SendDirectMessage;

public record SendDirectMessageResponse(Guid MessageId, MessageStatus MessageStatus);
