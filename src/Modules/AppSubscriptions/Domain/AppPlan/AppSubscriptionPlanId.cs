using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;

public sealed record AppSubscriptionPlanId(Guid Value) : TypedIdValueBase<Guid>(Value);