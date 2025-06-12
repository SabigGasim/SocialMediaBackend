using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.GetPayer;

public class GetPayerCommand(Guid payerId) : CommandBase<GetPayerResponse>
{
    public Guid PayerId { get; } = payerId;
}
