using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Follows;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Infrastructure;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common.Users;

namespace SocialMediaBackend.Modules.Feed.Tests.Core.Common;

public class FakeDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; } = default!;
    public DbSet<Follow> Follows { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<Comment> Comments { get; set; } = default!;
    public DbSet<FakeUserResource> FakeUserResources { get; set; } = default!;
    protected FakeDbContext() : base() {}
    public FakeDbContext(DbContextOptions<FakeDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.Fake);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IInfrastructureMarker).Assembly);

        modelBuilder.Entity<FakeUserResource>(builder =>
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => new(value)
                );

            builder.HasOne(x => x.Author)
                .WithMany()
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}