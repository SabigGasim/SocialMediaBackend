using Autofac;
using Microsoft.AspNetCore.SignalR;
using SocialMediaBackend.Modules.Chat.Application.Hubs;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations;

namespace SocialMediaBackend.Api;

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
