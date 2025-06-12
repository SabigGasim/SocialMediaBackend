using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Payments.Application.Contracts;
using SocialMediaBackend.Modules.Payments.Application.Payers.GetPayer;

namespace SocialMediaBackend.Api.Modules.Payments.Endpoints;

[HttpGet(ApiEndpoints.Payments.GetPayer)]
public class GetPayerEndpoint(IPaymentsModule module) : RequestEndpoint<GetPayerRequest, GetPayerResponse>(module)
{
    public override async Task HandleAsync(GetPayerRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new GetPayerCommand(req.PayerId), ct);
    }
}
