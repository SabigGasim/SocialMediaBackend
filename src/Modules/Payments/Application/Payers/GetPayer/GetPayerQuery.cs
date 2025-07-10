using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.GetPayer;

public class GetPayerQuery(Guid payerId) : QueryBase<GetPayerResponse>
{
    public Guid PayerId { get; } = payerId;
}
