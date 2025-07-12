using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.AppSubscriptions.Application.Contracts;
using SocialMediaBackend.Modules.AppSubscriptions.Application.Users.GetUser;

namespace SocialMediaBackend.Tests.SystemTests.CreateUserTests;

internal sealed class GetCreatedUserFromAppSubscriptionsProbe(Guid userId, IAppSubscriptionsModule module) : IProbe
{
    private readonly IAppSubscriptionsModule _module = module;
    private readonly Guid _userId = userId;

    private bool _actualExistsStatus = false;

    public bool IsSatisfied()
    {
        return _actualExistsStatus == true;
    }

    public async Task SampleAsync()
    {
        var res = await _module.ExecuteQueryAsync<CheckUserExistsQuery, UserExistsResponse>(
            new CheckUserExistsQuery(_userId));

        if (!res.IsSuccess || !res.Payload.Exists)
        {
            return;
        }

        _actualExistsStatus = true;
    }

    public string DescribeFailureTo()
    {
        return $"User with ID {_userId} was not created in app subscriptions module";
    }
}