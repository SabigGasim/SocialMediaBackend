using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.GetAllGroupMessages;

public record GetAllGroupMessagesRequest(Guid ChatId) : PagedRequest;
