using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Chat.Application.DirectMessaging.GetAllDirectMessages;

public record GetAllDirectMessagesRequest(Guid ChatId) : PagedRequest;
