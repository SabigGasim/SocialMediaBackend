using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.Modules.Chat.Application.DirectMessaging.MarkDirectMessageAsSeen;
using SocialMediaBackend.Modules.Chat.Application.Helpers;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations;

namespace SocialMediaBackend.Modules.Chat.Application.Hubs;

[Authorize]
public class ChatHub : Hub<IChatHub>
{
    private readonly IChatterRepository _chatterRepository;
    private readonly IChatRepository _chatRepository;
    private readonly ILifetimeScope _scope;

    public ChatHub()
    {
        _scope = ChatCompositionRoot.BeginLifetimeScope();
        _chatterRepository = _scope.Resolve<IChatterRepository>();
        _chatRepository = _scope.Resolve<IChatRepository>();
    }

    public override async Task OnConnectedAsync()
    {
        var chatterId = new ChatterId(Guid.Parse(Context.UserIdentifier!));

        var users = await _chatRepository.GetChattersWithDirectOrGroupChatWith(chatterId);

        await Clients.Users(users).UserConnected(chatterId.Value);

        await _chatterRepository.SetOnlineStatus(chatterId, true);
    }

    public async Task DirectChatNotifyTyping(Guid chatId, bool isTyping)
    {
        var senderId = new ChatterId(Guid.Parse(Context.UserIdentifier!));
        var directChatId = new DirectChatId(chatId);

        var receiverId = await _chatRepository.GetReceiverIdAsync(directChatId, senderId, Context.ConnectionAborted);

        if (receiverId is null)
        {
            //TODO: unauthorized
            Context.Abort();
        }

        await Clients.User(receiverId!).UpdateDirectChatTypingStatus(chatId, isTyping);
    }

    public async Task MarkDirectMessageAsSeen(Guid chatId, Guid lastSeenMessageId, bool isTyping)
    {
        var senderId = new ChatterId(Guid.Parse(Context.UserIdentifier!));
        var directChatId = new DirectChatId(chatId);
        var messageId = new DirectMessageId(lastSeenMessageId);

        var command = new MarkDirectMessageAsSeenCommand(directChatId, messageId);

        var result = await CommandExecutor.ExecuteAsync(command);

        if (!result.IsSuccess)
        {
            return;
        }

        var receiverId = await _chatRepository.GetReceiverIdAsync(directChatId, senderId);
        await Clients.User(receiverId!).UpdateDirectChatTypingStatus(chatId, isTyping);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var chatterId = new ChatterId(Guid.Parse(Context.UserIdentifier!));

        var users = await _chatRepository.GetChattersWithDirectOrGroupChatWith(chatterId);

        await Clients.Users(users).UserDisconnected(chatterId.Value);

        await _chatterRepository.SetOnlineStatus(chatterId, false);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        _scope.Dispose();
    }
}
