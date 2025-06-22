using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Users.Domain.AppPlan;

public record AppSubscriptionProductId(Guid Id) : TypedIdValueBase<Guid>(Id);
