﻿using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.Modules.Payments.Domain.Payers;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.CreatePayer;

internal sealed class CreatePayerCommandHandler(IAggregateRepository repository) : ICommandHandler<CreatePayerCommand>
{
    private readonly IAggregateRepository _repository = repository;

    public Task<HandlerResponse> ExecuteAsync(CreatePayerCommand command, CancellationToken ct)
    {
        var payer = Payer.Create(command.PayerId);

        _repository.StartStream(payer);

        return Task.FromResult((HandlerResponse)HandlerResponseStatus.NoContent);
    }
}
