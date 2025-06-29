using Dapper;
using Polly;
using Polly.Retry;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing.Messaging;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Messaging.Inbox;

internal sealed class ProcessInboxMessagesCommandHandler(
    IAggregateRepository repository,
    Mediator.IMediator mediator)
    : ICommandHandler<ProcessInboxMessagesCommand>
{
    private readonly IAggregateRepository _repository = repository;
    private readonly Mediator.IMediator _mediator = mediator;
    private readonly AsyncRetryPolicy _policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
            [
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(3)
            ]);

    public async Task<HandlerResponse> ExecuteAsync(ProcessInboxMessagesCommand command, CancellationToken ct)
    {
        var messages = await _repository.LoadManyAsync<InboxMessage, DateTimeOffset>(
            expression: x => x.Processed == false && x.Error == null,
            orderBy: x => x.OccurredOn,
            descending: false,
            ct);

        var messagesList = messages.AsList();

        foreach (var message in messagesList)
        {
            var result = await _policy.ExecuteAndCaptureAsync(() => ProcessMessage(message));
            message.ProcessedDate = DateTimeOffset.UtcNow;

            if (result.Outcome == OutcomeType.Successful)
            {
                message.Processed = true;
            }
            else
            {
                message.Processed = false;
                message.Error = result.FinalException.Message;
            }

            _repository.Store(message);
            await _repository.SaveChangesAsync(CancellationToken.None);
        }

        return HandlerResponseStatus.NoContent;
    }

    private async Task ProcessMessage(InboxMessage inboxMessage)
    {
        await _mediator.Publish(inboxMessage.Notification);
    }
}