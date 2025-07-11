using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Payments.Application.Contracts;
using SocialMediaBackend.Modules.Payments.Application.Payers.GetPayer;

namespace SocialMediaBackend.Tests.SystemTests.CreateUserTests;

internal sealed class GetCreatedUserFromPaymentsProbe(PayerInfo expectedPayerInfo, IPaymentsModule module) : IProbe
{
    private readonly PayerInfo _expectedPayerInfo = expectedPayerInfo;
    private readonly IPaymentsModule _module = module;

    private PayerInfo _actualUserInfo = default!;

    public bool IsSatisfied()
    {
        return _actualUserInfo == _expectedPayerInfo;
    }

    public async Task SampleAsync()
    {
        var res = await _module.ExecuteQueryAsync<GetPayerQuery, GetPayerResponse>(new GetPayerQuery(_expectedPayerInfo.Id));

        if (!res.IsSuccess)
        {
            return;
        }

        var payer = res.Payload;

        _actualUserInfo = new PayerInfo(
            payer.Id,
            payer.IsDeleted
            );
    }

    public string DescribeFailureTo()
    {
        return $"Payer with ID {_expectedPayerInfo.Id} was not created in payments module";
    }
}
