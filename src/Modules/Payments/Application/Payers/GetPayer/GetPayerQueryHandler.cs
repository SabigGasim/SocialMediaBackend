using Marten;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Payments.Domain.Payers;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.GetPayer;

internal sealed class GetPayerQueryHandler(IQuerySession session) : IQueryHandler<GetPayerQuery, GetPayerResponse>
{
    private readonly IQuerySession _session = session;

    public async Task<HandlerResponse<GetPayerResponse>> ExecuteAsync(GetPayerQuery command, CancellationToken ct)
    {
        var payer = await _session.LoadAsync<Payer>(command.PayerId, ct);

        if (payer is null)
        {
            return ("Payer with the given Id was not found", HandlerResponseStatus.NotFound);
        }

        return new GetPayerResponse(payer.Id, payer.IsDeleted);
    }
}
