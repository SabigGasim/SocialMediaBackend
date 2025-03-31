namespace SocialMediaBackend.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Domain.Entities;

public class ApplicationDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Media> Media { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .OwnsOne(u => u.ProfilePicture);

        modelBuilder.Entity<Post>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Post>()
            .OwnsMany(p => p.MediaItems);

        modelBuilder.Entity<Post>()
            .OwnsMany(p => p.Likes);

        modelBuilder.Entity<Comment>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany()
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.ParentComment)
            .WithMany()
            .HasForeignKey(c => c.ParentCommentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .OwnsMany(c => c.Likes);
    }
}

