using Marten;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.DeletePayer;

public class DeletePayerCommandHandler(IDocumentSession documentSession) : ICommandHandler<DeletePayerCommand>
{
    private readonly IDocumentSession _documentSession = documentSession;

    public Task<HandlerResponse> ExecuteAsync(DeletePayerCommand command, CancellationToken ct)
    {
        _documentSession.Events.Append(command.PayerId.Value, new PayerDeleted());

        return Task.FromResult((HandlerResponse)HandlerResponseStatus.NoContent);
    }
}
