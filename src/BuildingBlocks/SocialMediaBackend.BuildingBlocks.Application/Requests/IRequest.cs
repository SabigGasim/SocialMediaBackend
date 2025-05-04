namespace SocialMediaBackend.BuildingBlocks.Application.Requests;

public interface IRequestBase : FastEndpoints.ICommandBase;

public interface IRequest : FastEndpoints.ICommand, IRequestBase;

public interface IRequest<TResponse> : FastEndpoints.ICommand<TResponse>, IRequestBase;