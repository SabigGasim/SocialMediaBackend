﻿namespace SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

public abstract class CommandBase : ICommand<HandlerResponse>
{
    public Guid Id { get; }

    protected CommandBase(Guid id) => Id = id;

    protected CommandBase() => Id = Guid.NewGuid();
}

public abstract class CommandBase<TResponse> : ICommand<HandlerResponse<TResponse>>
{
    public Guid Id { get; }

    protected CommandBase(Guid id) => Id = id;

    protected CommandBase() => Id = Guid.NewGuid();
}
