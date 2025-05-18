using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.DirectMessaging.CreateDirectChat;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

[HttpPost(ApiEndpoints.DirectChat.CreateDirectChat)]
public class CreateDirectChatEndpoint(IChatModule module) : RequestEndpoint<CreateDirectChatRequest, CreateDirectChatResponse>(module)
{
    public override async Task HandleAsync(CreateDirectChatRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new CreateDirectChatCommand(req.RecipientId), ct);
    }
}
