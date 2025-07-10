using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Chatters;

public class ChatterContext(IExecutionContextAccessor context) : IChatterContext
{
    private readonly IExecutionContextAccessor _context = context;

    public ChatterId ChatterId => new ChatterId(_context.UserId);
}
