namespace SocialMediaBackend.Modules.AppSubscriptions.Application.AppPlans.CreateAppPlan;

public record PriceRequest(int Amount, string Currency, string Interval);
public record CreateAppPlanRequest(IEnumerable<PriceRequest> Prices, string Tier);
