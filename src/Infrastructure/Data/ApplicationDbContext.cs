using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Domain.Feed.Posts;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Users.Follows;

namespace SocialMediaBackend.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;  
    public DbSet<Comment> Comments { get; set; } = default!;
    public DbSet<Follow> Follows { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected ApplicationDbContext() {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IInfrastructureMarker).Assembly);
    }
}

