using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.MarkDirectMessageAsSeen;
using SocialMediaBackend.Modules.Chat.Application.Helpers;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
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
        var connectionTracker = _scope.Resolve<IHubConnectionTracker>();

        await connectionTracker.AddConnectionAsync(Context.UserIdentifier!, Context.ConnectionId);

        var chatterId = new ChatterId(Guid.Parse(Context.UserIdentifier!));

        var users = await _chatRepository.GetChattersWithDirectOrGroupChatWith(chatterId);

        await Clients.Users(users).UserConnected(chatterId.Value);

        var groups = await _chatRepository.GetJoinedUserGroupChats(chatterId);

        await Task.WhenAll(
            groups.Select(x => Groups.AddToGroupAsync(Context.ConnectionId, x, Context.ConnectionAborted))
        );

        await _chatterRepository.SetOnlineStatus(chatterId, true);
    }

    public async Task JoinGroup(Guid groupChatId)
    {
        var chatterId = new ChatterId(Guid.Parse(Context.UserIdentifier!));
        var groupId = new GroupChatId(groupChatId);

        var handler = _scope.Resolve<IAuthorizationHandler<GroupChat, GroupChatId>>();

        if (await handler.AuthorizeAsync(chatterId, groupId, Context.ConnectionAborted))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupChatId.ToString(), Context.ConnectionAborted);
        }
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
        var connectionTracker = _scope.Resolve<IHubConnectionTracker>();
        
        await connectionTracker.RemoveConnectionAsync(Context.UserIdentifier!, Context.ConnectionId);

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
