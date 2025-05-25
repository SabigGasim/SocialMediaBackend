using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;
using SocialMediaBackend.Modules.Feed.Application.Mappings;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.CreatePost;

public class CreatePostCommandHandler(FeedDbContext context) : ICommandHandler<CreatePostCommand, CreatePostResponse>
{
    private readonly FeedDbContext _context = context;

    public async Task<HandlerResponse<CreatePostResponse>> ExecuteAsync(CreatePostCommand command, CancellationToken ct)
    {
        var user = await _context.Authors.FindAsync([new AuthorId(command.UserId)], ct);

        var result = user!.AddPost(command.Text, command.MediaItems.Select(x => Media.Create(x)));
        if (!result.IsSuccess)
        {
            return result;
        }

        var post = result.Payload;

        _context.Posts.Add(post);

        return (post.MapToCreateResponse(), HandlerResponseStatus.Created);
    }
}
