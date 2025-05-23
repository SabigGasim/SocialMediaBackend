using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectMessage;

public record SendDirectMessageResponse(Guid MessageId, MessageStatus MessageStatus);
