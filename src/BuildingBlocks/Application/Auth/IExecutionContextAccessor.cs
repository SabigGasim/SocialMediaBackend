namespace SocialMediaBackend.BuildingBlocks.Application.Auth;

public interface IExecutionContextAccessor
{
    Guid UserId { get; }

    bool IsAvailable { get; }
}