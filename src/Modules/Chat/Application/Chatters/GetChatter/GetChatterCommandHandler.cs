using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Application.Mappings;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;

public class GetChatterCommandHandler(IChatterRepository chatterRepository)
    : ICommandHandler<GetChatterCommand, GetChatterResponse>
{
    private readonly IChatterRepository _chatterRepository = chatterRepository;

    public async Task<HandlerResponse<GetChatterResponse>> ExecuteAsync(GetChatterCommand command, CancellationToken ct)
    {
        var chatter = await _chatterRepository.GetByIdAsync(command.ChatterId, ct); ;
        if (chatter is null)
        {
            return ("Chatter with the given Id was not found", HandlerResponseStatus.NotFound, command.ChatterId.Value);
        }

        return chatter.MapToResponse();
    }
}
