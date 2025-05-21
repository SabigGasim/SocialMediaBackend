namespace SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

public interface ISingleUserCommandHandler<TCommand, TMessage, TResult> : ICommandHandler<TCommand, SingleUserResponse<TMessage, TResult>>
    where TCommand : ICommand<HandlerResponse<SingleUserResponse<TMessage, TResult>>>
    where TMessage : IRealtimeMessage
{

}

public interface ISingleUserCommandHandler<TCommand, TMessage> : ICommandHandler<TCommand, SingleUserResponse<TMessage>>
    where TCommand : ICommand<HandlerResponse<SingleUserResponse<TMessage>>>
    where TMessage : IRealtimeMessage
{

}

public interface IAllUsersCommandHandler<TCommand, TMessage, TResult> : ICommandHandler<TCommand, AllUsersResponse<TMessage, TResult>>
    where TCommand : ICommand<HandlerResponse<AllUsersResponse<TMessage, TResult>>>
    where TMessage : IRealtimeMessage
{

}

public interface IAllUsersCommandHandler<TCommand, TMessage> : ICommandHandler<TCommand, AllUsersResponse<TMessage>>
    where TCommand : ICommand<HandlerResponse<AllUsersResponse<TMessage>>>
    where TMessage : IRealtimeMessage
{

}

public interface IMultipleUsersCommandHandler<TCommand, TMessage, TResult> : ICommandHandler<TCommand, MultipleUsersResponse<TMessage, TResult>>
    where TCommand : ICommand<HandlerResponse<MultipleUsersResponse<TMessage, TResult>>>
    where TMessage : IRealtimeMessage
{

}

public interface IMultipleUsersCommandHandler<TCommand, TMessage> : ICommandHandler<TCommand, MultipleUsersResponse<TMessage>>
    where TCommand : ICommand<HandlerResponse<MultipleUsersResponse<TMessage>>>
    where TMessage : IRealtimeMessage
{

}

public interface IGroupCommandHandler<TCommand, TMessage, TResult> : ICommandHandler<TCommand, GroupResponse<TMessage, TResult>>
    where TCommand : ICommand<HandlerResponse<GroupResponse<TMessage, TResult>>>
    where TMessage : IRealtimeMessage
{

}

public interface IGroupCommandHandler<TCommand, TMessage> : ICommandHandler<TCommand, GroupResponse<TMessage>>
    where TCommand : ICommand<HandlerResponse<GroupResponse<TMessage>>>
    where TMessage : IRealtimeMessage
{

}
