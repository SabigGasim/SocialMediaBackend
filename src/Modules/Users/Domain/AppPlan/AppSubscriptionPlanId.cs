using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Users.Domain.AppPlan;

public record AppSubscriptionPlanId(Guid Value) : TypedIdValueBase<Guid>(Value);