using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Chat.Application.GroupMessaging.GetAllGroupMessages;

public record GetAllGroupMessagesRequest(Guid ChatId) : PagedRequest;
