using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Users.Domain.Feed;
using SocialMediaBackend.Modules.Users.Domain.Feed.Comments;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using SocialMediaBackend.Modules.Users.Infrastructure.Domain.Authors;
using SocialMediaBackend.Modules.Users.Infrastructure.Domain.Comments;
using SocialMediaBackend.Modules.Users.Infrastructure.Domain.Posts;
using SocialMediaBackend.Modules.Users.Infrastructure.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Author> Authors { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;  
    public DbSet<Comment> Comments { get; set; } = default!;
    public DbSet<Follow> Follows { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected ApplicationDbContext() {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new FollowEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PostLikeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CommentLikeEntityTypeConfiguration());
    }
}

