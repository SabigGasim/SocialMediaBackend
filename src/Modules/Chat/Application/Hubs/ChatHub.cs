using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.MarkDirectMessageAsSeen;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessagAsSeen;
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
    private readonly IHubConnectionTracker _connectionTracker;

    public ChatHub(IHubConnectionTracker hubConnectionTracker)
    {
        _scope = ChatCompositionRoot.BeginLifetimeScope();
        _chatterRepository = _scope.Resolve<IChatterRepository>();
        _chatRepository = _scope.Resolve<IChatRepository>();
        _connectionTracker = hubConnectionTracker;
    }

    public override async Task OnConnectedAsync()
    {
        await _connectionTracker.AddConnectionAsync(Context.UserIdentifier!, Context.ConnectionId);

        var chatterId = new ChatterId(Guid.Parse(Context.UserIdentifier!));

        var users = await _chatRepository.GetChattersWithDirectOrGroupChatWith(chatterId);

        await Clients.Users(users).UserConnected(chatterId.Value);

        var groups = await _chatRepository.GetJoinedUserGroupChats(chatterId);

        foreach (var group in groups)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group, Context.ConnectionAborted);
        }

        await _chatterRepository.SetOnlineStatus(chatterId, true);
    }

    public async Task DirectChatNotifyTyping(Guid chatId, bool isTyping)
    {
        var senderId = new ChatterId(Guid.Parse(Context.UserIdentifier!));
        var directChatId = new DirectChatId(chatId);

        var receiverId = await _chatRepository.GetReceiverIdAsync(directChatId, senderId, Context.ConnectionAborted);

        if (receiverId is null)
        {
            return;
        }

        await Clients.User(receiverId!).UpdateDirectChatTypingStatus(chatId, isTyping);
    }

    public async Task GroupChatNotifyTyping(Guid chatId, bool isTyping)
    {
        var senderId = new ChatterId(Guid.Parse(Context.UserIdentifier!));
        var groupChatId = new GroupChatId(chatId);

        var handler = _scope.Resolve<IAuthorizationHandler<GroupChat, GroupChatId>>();

        var authorizationResult = await handler.AuthorizeAsync(senderId, groupChatId);

        if (!authorizationResult.IsSuccess)
        {
            return;
        }

        await Clients.OthersInGroup(chatId.ToString()).UpdateGroupChatTypingStatus(chatId, isTyping);
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
        var message = new MarkDirectMessageAsSeenMessage(chatId, lastSeenMessageId);

        await Clients.User(receiverId!).NotifyDirectMessageSeen(message);
    }

    public async Task MarkGroupMessageAsSeen(Guid groupId, Guid messageId)
    {
        var command = new MarkGroupMessageAsSeenCommand(groupId, messageId);

        var result = await CommandExecutor.ExecuteAsync(command);

        if (!result.IsSuccess)
        {
            return;
        }

        var groupName = groupId.ToString();
        var message = new MarkGroupMessagAsSeenMessage(
            groupId, 
            messageId, 
            Guid.Parse(Context.UserIdentifier!));

        await Clients.OthersInGroup(groupName).NotifyGroupMessageSeen(message);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await _connectionTracker.RemoveConnectionAsync(Context.UserIdentifier!, Context.ConnectionId);

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
