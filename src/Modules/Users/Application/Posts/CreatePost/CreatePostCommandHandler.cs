using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Common;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Domain.Common.ValueObjects;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Posts.CreatePost;

public class CreatePostCommandHandler(ApplicationDbContext context) : ICommandHandler<CreatePostCommand, CreatePostResponse>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse<CreatePostResponse>> ExecuteAsync(CreatePostCommand command, CancellationToken ct)
    {
        var user = await _context.Authors.FindAsync([command.UserId], ct);
        
        var post = user!.AddPost(command.Text, command.MediaItems.Select(x => Media.Create(x.Url)));
        if (post is null)
        {
            return ("An error occured while creating the post", HandlerResponseStatus.InternalError);
        }

        _context.Posts.Add(post);

        await _context.SaveChangesAsync(ct);

        return (post.MapToCreateResponse(), HandlerResponseStatus.Created);
    }
}
