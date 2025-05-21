namespace SocialMediaBackend.BuildingBlocks.Application.Requests;

public enum Recipients
{
    All, MultipleUsers, SingleUser, Group
}

public interface IRealtimeResponse<TMessage>
{
    TMessage Message { get; }
    string Method { get; }
    Recipients Recipients { get; }
}

public interface IRealtimeResponse<TMessage, TIdentifier> : IRealtimeResponse<TMessage>
{
    TIdentifier ReceiverId { get; }
}

public interface IRealtimeWithPayloadResponse<TMessage, TPayload> : IRealtimeResponse<TMessage>
{
    TPayload Payload { get; }
}

public interface IRealtimeWithPayloadResponse<TMessage, TPayload, TIdentifier> 
    : IRealtimeWithPayloadResponse<TMessage, TPayload>, IRealtimeResponse<TMessage, TIdentifier>;

public abstract class RealtimeResponse<TMessage> : IRealtimeResponse<TMessage>
{
    public required TMessage Message { get; init; }
    public required string Method { get; init; }
    public abstract Recipients Recipients { get; }
}

public abstract class RealtimeResponse<TMessage, TIdentifier> : RealtimeResponse<TMessage>, IRealtimeResponse<TMessage, TIdentifier>
{
    public required TIdentifier ReceiverId { get; init; }
}

public abstract class RealtimeResponse<TMessage, TPayload, TIdentifier> 
    : RealtimeResponse<TMessage, TIdentifier>, IRealtimeWithPayloadResponse<TMessage, TPayload, TIdentifier>
{
    public required TPayload Payload { get; init; }
}

public class MultipleUsersResponse<TMessage> : RealtimeResponse<TMessage, IEnumerable<string>>
{
    public override Recipients Recipients { get; } = Recipients.MultipleUsers;
}

public class MultipleUsersResponse<TMessage, TPayload> : RealtimeResponse<TMessage, TPayload, IEnumerable<string>>
{
    public override Recipients Recipients { get; } = Recipients.MultipleUsers;
}

public class SingleUserResponse<TMessage> : RealtimeResponse<TMessage, string>
{
    public override Recipients Recipients { get; } = Recipients.SingleUser;
}

public class SingleUserResponse<TMessage, TPayload> : RealtimeResponse<TMessage, TPayload, string>, IRealtimeWithPayloadResponse<TMessage, TPayload, string>
{
    public override Recipients Recipients { get; } = Recipients.SingleUser;
}

public class GroupResponse<TMessage> : RealtimeResponse<TMessage, string>
{
    public override Recipients Recipients { get; } = Recipients.Group;
}

public class GroupResponse<TMessage, TPayload> : RealtimeResponse<TMessage, TPayload, string>
{
    public override Recipients Recipients { get; } = Recipients.Group;
}

public class AllUsersResponse<TMessage> : RealtimeResponse<TMessage>
{
    public override Recipients Recipients { get; } = Recipients.All;
}

public class AllUsersResponse<TMessage, TPayload> : RealtimeResponse<TMessage>, IRealtimeWithPayloadResponse<TMessage, TPayload>
{
    public override Recipients Recipients { get; } = Recipients.All;
    public required TPayload Payload { get; init; }
}