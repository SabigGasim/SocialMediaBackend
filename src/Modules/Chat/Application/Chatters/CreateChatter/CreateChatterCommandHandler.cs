using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Roles;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.CreateChatter;

internal sealed class CreateChatterCommandHandler(ChatDbContext context) : ICommandHandler<CreateChatterCommand>
{
    private readonly ChatDbContext _context = context;

    public Task<HandlerResponse> ExecuteAsync(CreateChatterCommand command, CancellationToken ct)
    {
        var chatter = Chatter.Create(
            command.ChatterId,
            command.Username,
            command.Nickname,
            command.ProfilePicture,
            command.ProfileIsPublic,
            command.FollowersCount,
            command.FollowingCount);

        _context.Add(chatter);
        _context.Set<ChatterRole>().Add(new ChatterRole(Roles.Chatter, chatter.Id));

        return Task.FromResult((HandlerResponse)HandlerResponseStatus.NoContent);
    }
}
