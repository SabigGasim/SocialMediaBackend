using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Chat.Application.Mappings;
using SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;

internal sealed class GetChatterQueryHandler(IChatterRepository chatterRepository)
    : IQueryHandler<GetChatterQuery, GetChatterResponse>
{
    private readonly IChatterRepository _chatterRepository = chatterRepository;

    public async Task<HandlerResponse<GetChatterResponse>> ExecuteAsync(GetChatterQuery command, CancellationToken ct)
    {
        var chatter = await _chatterRepository.GetByIdAsync(command.ChatterId, ct); ;
        if (chatter is null)
        {
            return ("Chatter with the given Id was not found", HandlerResponseStatus.NotFound, command.ChatterId.Value);
        }

        return chatter.MapToResponse();
    }
}
