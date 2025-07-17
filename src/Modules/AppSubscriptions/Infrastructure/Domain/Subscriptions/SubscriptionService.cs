using Dapper;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Subscriptions;

internal sealed class SubscriptionService(IDbConnectionFactory factory) : ISubscriptionService
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<Result> CanIssueSubscriptionPaymentIntentAsync(UserId userId, CancellationToken ct = default)
    {
        const string sql = $"""
            WITH payment_check AS (
                SELECT EXISTS (
                    SELECT 1
                    FROM {Schema.AppSubscriptions}."SubscriptionPayments"
                    WHERE "PayerId" = @PayerId
                      AND "PaymentStatus" = @PaymentIntentRequestedStatus
                ) AS exists_payment
            ),
            subscription_check AS (
                SELECT EXISTS (
                    SELECT 1
                    FROM {Schema.AppSubscriptions}."Subscriptions"
                    WHERE "SubscriberId" = @PayerId
                      AND "Status" != @CanceledStatus
                ) AS exists_subscription
            )
            SELECT
                CASE
                    WHEN pc.exists_payment      THEN 1 -- PaymentIntentExists
                    WHEN sc.exists_subscription THEN 2 -- AlreadySubscribed
                    ELSE 0 -- Succeed
                END AS Status
            FROM payment_check pc, subscription_check sc;
            """;


        using (var connection = await _factory.CreateAsync(ct))
        {
            var result = await connection.ExecuteScalarAsync<int>(new CommandDefinition(sql, new
            {
                PayerId = userId.Value,
                PaymentIntentRequestedStatus = (int)SubscriptionPaymentStatus.PaymentIntentRequested,
                CanceledStatus = (int)SubscriptionStatus.Canceled
            }, cancellationToken: ct));
            
            var status = (SubscriptionCheckStatus)result;

            return status switch
            {
                SubscriptionCheckStatus.PaymentIntentExists =>
                    Result.Conflict("You have already requested a subscription payment, please proceed with it"),

                SubscriptionCheckStatus.AlreadySubscribed =>
                    Result.Conflict("You are already subscribed to a plan, please cancel it or upgrade it"),

                _ => Result.Success()
            };
        }
    }

    private enum SubscriptionCheckStatus
    {
        None = 0,
        PaymentIntentExists = 1,
        AlreadySubscribed = 2
    }
}
