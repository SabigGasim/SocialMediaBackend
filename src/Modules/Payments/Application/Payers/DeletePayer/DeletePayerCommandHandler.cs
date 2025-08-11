using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.Modules.Payments.Domain.Payers;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.DeletePayer;

internal sealed class DeletePayerCommandHandler(IAggregateRepository repository) : ICommandHandler<DeletePayerCommand>
{
    private readonly IAggregateRepository _repository = repository;

    public async Task<HandlerResponse> ExecuteAsync(DeletePayerCommand command, CancellationToken ct)
    {
        var payer = await _repository.LoadAsync<Payer>(command.PayerId.Value, CancellationToken.None);

        payer.Delete();

        return HandlerResponseStatus.Deleted;
    }
}
