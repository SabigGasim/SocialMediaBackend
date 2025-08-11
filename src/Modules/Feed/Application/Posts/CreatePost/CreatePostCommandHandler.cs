using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;
using SocialMediaBackend.Modules.Feed.Application.Mappings;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.CreatePost;

internal sealed class CreatePostCommandHandler(
    FeedDbContext context,
    IAuthorContext authorContext) : ICommandHandler<CreatePostCommand, CreatePostResponse>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorContext _authorContext = authorContext;

    public async Task<HandlerResponse<CreatePostResponse>> ExecuteAsync(CreatePostCommand command, CancellationToken ct)
    {
        var author = await _context.Authors.FirstAsync(x => x.Id == _authorContext.AuthorId, ct);

        var result = author.CreatePost(command.Text, command.MediaItems.Select(x => Media.Create(x)));
        if (!result.IsSuccess)
        {
            return result;
        }

        var post = result.Payload;

        _context.Posts.Add(post);

        return (post.MapToCreateResponse(), HandlerResponseStatus.Created);
    }
}
