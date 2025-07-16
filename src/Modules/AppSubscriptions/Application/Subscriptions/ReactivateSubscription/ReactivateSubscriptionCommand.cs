using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.ReactivateSubscription;

[HasPermission(Permissions.ReactivateAppSubscription)]
public sealed class ReactivateSubscriptionCommand : CommandBase, IRequireAuthorization;
