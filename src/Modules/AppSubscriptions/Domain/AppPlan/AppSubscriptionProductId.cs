using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;

public record AppSubscriptionProductId(Guid Id) : TypedIdValueBase<Guid>(Id);
