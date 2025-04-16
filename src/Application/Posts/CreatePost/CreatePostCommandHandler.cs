using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Domain.Common.ValueObjects;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Posts.CreatePost;

public class CreatePostCommandHandler(ApplicationDbContext context) : ICommandHandler<CreatePostCommand, CreatePostResponse>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse<CreatePostResponse>> ExecuteAsync(CreatePostCommand command, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == command.UserId, ct);
        if (user is null)
        {
            return ("User was not found", HandlerResponseStatus.BadRequest, command.UserId);
        }
        
        var post = user.AddPost(command.Text, command.MediaItems.Select(x => Media.Create(x.Url)));
        if (post is null)
        {
            return ("An error occured while creating the post", HandlerResponseStatus.InternalError);
        }

        _context.Posts.Add(post);

        await _context.SaveChangesAsync(ct);

        return (post.MapToCreateResponse(), HandlerResponseStatus.Created);
    }
}
