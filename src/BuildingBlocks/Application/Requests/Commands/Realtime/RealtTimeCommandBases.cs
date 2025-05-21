namespace SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

public class SingleUserCommandBase<TMessage, TPayload> : CommandBase<SingleUserResponse<TMessage, TPayload>>
    where TMessage : IRealtimeMessage;
public class SingleUserCommandBase<TMessage> : CommandBase<SingleUserResponse<TMessage>>
    where TMessage : IRealtimeMessage;

public class AllUsersCommandBase<TMessage, TPayload> : CommandBase<AllUsersResponse<TMessage, TPayload>>
    where TMessage : IRealtimeMessage;
public class AllUsersCommandBase<TMessage> : CommandBase<AllUsersResponse<TMessage>>
    where TMessage : IRealtimeMessage;

public class MultipleUsersCommandBase<TMessage, TPayload> : CommandBase<MultipleUsersResponse<TMessage, TPayload>>
    where TMessage : IRealtimeMessage;
public class MultipleUsersCommandBase<TMessage> : CommandBase<MultipleUsersResponse<TMessage>>
    where TMessage : IRealtimeMessage;

public class GroupCommandBase<TMessage, TPayload> : CommandBase<GroupResponse<TMessage, TPayload>>
    where TMessage : IRealtimeMessage;
public class GroupCommandBase<TMessage> : CommandBase<GroupResponse<TMessage>>
    where TMessage : IRealtimeMessage;