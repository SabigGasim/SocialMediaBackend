using Polly;
using Polly.Retry;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Helpers;
using SocialMediaBackend.Modules.Payments.Domain.Products;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;
using SocialMediaBackend.Modules.Payments.Infrastructure;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.CleanUpPayerResources;

internal sealed class CleanUpPayerResourcesCommandHandler(
    IAggregateRepository repository,
    IPaymentService paymentService)
    : ICommandHandler<CleanUpPayerResourcesCommand>
{
    private readonly IAggregateRepository _repository = repository;
    private readonly IPaymentService _paymentService = paymentService;
    private readonly AsyncRetryPolicy _retryPolicy = GetRetryPolicy();

    public async Task<HandlerResponse> ExecuteAsync(CleanUpPayerResourcesCommand command, CancellationToken ct)
    {
        var payerId = command.PayerId.Value;

        var productsTask = _repository.LoadManyAsync<Product>(
            x => x.Owner == payerId.ToString() && x.IsDeleted == false);

        var subscriptionsTask = _repository.LoadManyAsync<Subscription>(x =>
            x.PayerId == payerId && x.Status != SubscriptionStatus.Cancelled);
        
        var paymentMethodsTask = _paymentService.GetCustomerPaymentMethodIdsAsync(command.GatewayCustomerId);

        await Task.WhenAll(productsTask, subscriptionsTask, paymentMethodsTask);

        var payerProducts = await productsTask;
        var payerSubscriptions = await subscriptionsTask;
        var paymentMethodIds = await paymentMethodsTask;

        //Running the following tasks with Task.WhenAll could possibly cause rate limitting

        await payerSubscriptions
            .Select(CancelSubscriptionAsync())
            .ProcessIndividually($"Couldn't cancel subscription made by delete user {payerId}");

        await paymentMethodIds
            .Select(DeletePaymentMethodAsync())
            .ProcessIndividually($"Couldn't remove payment method for user {payerId}");

        await payerProducts
            .Select(ArchiveProductAsync())
            .ProcessIndividually(
                product => product.MarkAsDeleted(),
                $"Couldn't archive product owned by deleted user {payerId}");
        
        return HandlerResponseStatus.OK;
    }

    private Func<string, Task> DeletePaymentMethodAsync()
    {
        return methodId => _retryPolicy.ExecuteAsync(() =>
                    _paymentService.DeletePaymentMethodAsync(methodId));
    }

    private Func<Product, Task<Product>> ArchiveProductAsync()
    {
        return async product =>
        {
            await _retryPolicy.ExecuteAsync(() => _paymentService.ArchiveProductAsync(product.GatewayProductId));
            return product;
        };
    }

    private Func<Subscription, Task> CancelSubscriptionAsync()
    {
        return subscription =>
                    _retryPolicy.ExecuteAsync(() => _paymentService
                        .CancelSubscriptionAsync(subscription.GatewaySubscriptionId));
    }

    private static AsyncRetryPolicy GetRetryPolicy()
    {
        return Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                [
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(4),
                    TimeSpan.FromSeconds(8),
                ],
                onRetry: (exception, timespan, attempt, _) =>
                {
                    Console.Error.WriteLine($"Retry {attempt} after {timespan.TotalSeconds}s due to: {exception.Message}");
                });
    }
}
