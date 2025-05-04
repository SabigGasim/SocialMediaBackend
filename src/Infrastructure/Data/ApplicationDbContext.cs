using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Domain.Feed;
using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Domain.Feed.Posts;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Users.Follows;
using SocialMediaBackend.Infrastructure.Domain.Authors;
using SocialMediaBackend.Infrastructure.Domain.Comments;
using SocialMediaBackend.Infrastructure.Domain.Posts;
using SocialMediaBackend.Infrastructure.Domain.Users;

namespace SocialMediaBackend.Infrastructure.Data;
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

