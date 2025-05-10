using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Data;

public class FeedDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<Comment> Comments { get; set; } = default!;
    public DbSet<InternalCommand> InternalCommands { get; set; } = default!;

    protected FeedDbContext() {}

    public FeedDbContext(DbContextOptions<FeedDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.Feed);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IInfrastructureMarker).Assembly);
    }
}
