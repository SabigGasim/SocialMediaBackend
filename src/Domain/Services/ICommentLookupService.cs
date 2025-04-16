using SocialMediaBackend.Domain.Comments;

namespace SocialMediaBackend.Domain.Services;

public interface ICommentLookupService
{
    Task<bool> ExistsAsync(Guid commentId, CancellationToken ct = default);
    Task<Comment?> FindAsync(
        Guid commentId, 
        bool includeLikes = false,
        bool includeParent = false,
        CancellationToken ct = default);
    Task<Comment?> FindCommentLikedByUser(
        Guid commentId,
        Guid userId,
        bool includeParent = false,
        CancellationToken ct = default);
}
