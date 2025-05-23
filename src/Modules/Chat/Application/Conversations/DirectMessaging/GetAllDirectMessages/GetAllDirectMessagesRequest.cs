using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.GetAllDirectMessages;

public record GetAllDirectMessagesRequest(Guid ChatId) : PagedRequest;
