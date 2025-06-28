using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Application.Mappings;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.GetAllDirectMessages;

internal sealed class GetAllDirectMessagesQueryHandler(
    IChatRepository chatRepository,
    IAuthorizationHandler<DirectChat, DirectChatId> authorizationHandler)
    : IQueryHandler<GetAllDirectMessagesQuery, GetAllDirectMessagesResponse>
{
    private readonly IChatRepository _chatRepository = chatRepository;
    private readonly IAuthorizationHandler<DirectChat, DirectChatId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse<GetAllDirectMessagesResponse>> ExecuteAsync(GetAllDirectMessagesQuery query, CancellationToken ct)
    {
        var chatterId = new ChatterId(query.UserId);

        var authorizationResult = await _authorizationHandler.AuthorizeAsync(chatterId, query.DirectChatId, ct);

        if (!authorizationResult.IsSuccess)
        {
            return authorizationResult;
        }

        var messages = await _chatRepository
            .GetAllDirectChatMessages(chatterId, query.DirectChatId, query.Page, query.PageSize, ct);

        return messages.MapToResponse();
    }
}
