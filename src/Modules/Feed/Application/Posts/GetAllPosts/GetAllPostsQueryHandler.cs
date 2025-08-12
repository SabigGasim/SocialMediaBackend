using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Feed.Application.Mappings;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.GetAllPosts;

internal sealed class GetAllPostsQueryHandler(
    IPostRepository repository,
    IOptionalAuthorContext optionalAuthorContext,
    IPermissionManager permissionManager)
    : IQueryHandler<GetAllPostsQuery, GetAllPostsResponse>
{
    private readonly IPostRepository _repository = repository;
    private readonly IOptionalAuthorContext _optionalAuthorContext = optionalAuthorContext;
    private readonly IPermissionManager _permissionManager = permissionManager;

    public async Task<HandlerResponse<GetAllPostsResponse>> ExecuteAsync(GetAllPostsQuery query, CancellationToken ct)
    {
        var options = new GetAllPostsOptions
        {
            Page = query.Page,
            PageSize = query.PageSize,
            IdOrUsername = query.IdOrUsername,
            Order = query.Order,
            Since = query.Since,
            Until = query.Until,
            Text = query.Text,
        };

        if (_optionalAuthorContext.AuthorId is not null)
        {
            options.RequestingUserId = _optionalAuthorContext.AuthorId.Value;
            options.IsAdmin = await _permissionManager.UserIsInRole(
                _optionalAuthorContext.AuthorId.Value,
                (int)Roles.AdminAuthor,
                ct);
        }

        var (Items, Page, PageSize, TotalCount) = await _repository.GetAllAsync(options, ct);
        var response = Items.MapToResponse(Page, PageSize, TotalCount);

        return (response, HandlerResponseStatus.OK);
    }
}
