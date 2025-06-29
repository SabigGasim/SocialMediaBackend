using Mediator;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.Payers.Events;
using SocialMediaBackend.Modules.Payments.Infrastructure;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.CreatePayer;

internal sealed class PayerCreatedDomainEventHandler(
    IAggregateRepository repository,
    IPaymentService paymentService,
    IHostEnvironment env)
    : INotificationHandler<PayerCreatedDomainEvent>
{
    private readonly IAggregateRepository _repository = repository;
    private readonly IPaymentService _paymentService = paymentService;
    private readonly IHostEnvironment _env = env;

    public async ValueTask Handle(PayerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (_env.IsEnvironment("Testing"))
        {
            return;
        }

        var customer = await _paymentService.CreateCustomerAsync(notification.PayerId);

        var payer = await _repository.LoadAsync<Payer>(notification.PayerId.Value, cancellationToken);
        
        ArgumentNullException.ThrowIfNull(payer, nameof(payer));

        payer.RegisterCustomer(customer.Id);
    }
}
