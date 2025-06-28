using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Application.Mappings;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectChat;

internal sealed class CreateDirectChatCommandHandler : ICommandHandler<CreateDirectChatCommand, CreateDirectChatResponse>
{
    private readonly ChatDbContext _context;
    private readonly IChatterRepository _chatterRepository;
    private readonly IChatRepository _chatRepository;

    public CreateDirectChatCommandHandler(
        ChatDbContext context,
        IChatterRepository chatterRepository,
        IChatRepository chatRepository)
    {
        _context = context;
        _chatterRepository = chatterRepository;
        _chatRepository = chatRepository;
    }

    public async Task<HandlerResponse<CreateDirectChatResponse>> ExecuteAsync(CreateDirectChatCommand command, CancellationToken ct)
    {
        if (!await _chatterRepository.ExistsAsync(command.OtherChatterId, ct))
        {
            return ("User with the given Id was not found", HandlerResponseStatus.NotFound, command.OtherChatterId);
        }

        var firstChatterId = new ChatterId(command.UserId);

        if (await _chatRepository.DirectChatExistsAsync(firstChatterId, command.OtherChatterId, ct))
        {
            return ("A direct chat with the same users already exists", HandlerResponseStatus.Conflict);
        }

        var chat = DirectChat.Create(firstChatterId, command.OtherChatterId, DateTimeOffset.UtcNow);

        _context.Add(chat);

        return (chat.MapToResponse(), HandlerResponseStatus.Created);
    }
}
