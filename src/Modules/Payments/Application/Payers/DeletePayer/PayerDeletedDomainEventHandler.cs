using Mediator;
using Polly;
using Polly.Retry;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.Modules.Payments.Domain.Payers.Events;
using SocialMediaBackend.Modules.Payments.Domain.Products;
using SocialMediaBackend.Modules.Payments.Infrastructure;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.DeletePayer;

public class PayerDeletedDomainEventHandler(
    IAggregateRepository repository,
    IPaymentService paymentService) : INotificationHandler<PayerDeletedDomainEvent>
{
    private readonly IAggregateRepository _repository = repository;
    private readonly IPaymentService _paymentService = paymentService;
    private readonly AsyncRetryPolicy _retryPolicy = GetRetryPolicy();

    public async ValueTask Handle(PayerDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var payerId = notification.PayerId.Value.ToString();
        var payerProducts = await _repository.LoadManyAsync<Product>(x => x.Owner == payerId);

        var archiveTasks = payerProducts.Select(async product =>
        {
            await _retryPolicy.ExecuteAsync(() => _paymentService.ArchiveProductAsync(product.GatewayProductId));
            return product;
        });

        await foreach (var productTask in Task.WhenEach(archiveTasks))
        {
            try
            {
                productTask.Result.MarkAsDeleted();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error archiving product owned by deleted payer with Id {payerId}: {ex.Message}");
            }
        }
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
                    Console.WriteLine($"Retry {attempt} after {timespan.TotalSeconds}s due to: {exception.Message}");
                });
    }
}
