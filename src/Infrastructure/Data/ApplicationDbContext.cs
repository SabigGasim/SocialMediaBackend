using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }  
    public DbSet<Comment> Comments { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected ApplicationDbContext()
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        UserModelConfiguration(modelBuilder);
        PostModelConfiguration(modelBuilder);
        CommentModelConfiguration(modelBuilder);
        PostLikeModelConfiguration(modelBuilder);
        CommentLikeModelConfiguration(modelBuilder);
    }

    private static void CommentModelConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>()
                    .HasKey(c => c.Id);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void PostModelConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
                    .HasKey(u => u.Id);

        modelBuilder.Entity<Post>(post =>
        {
            post.OwnsMany(p => p.MediaItems, m =>
            {
                m.WithOwner().HasForeignKey("PostId");
                m.Property<Guid>("Id");
                m.HasKey("Id");
                m.Property(p => p.Url).HasColumnName("Url");
                m.Property(p => p.FilePath).HasColumnName("FilePath");
                m.Property(p => p.MediaType).HasColumnName("MediaType");
            });
        });

        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void PostLikeModelConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostLike>()
            .HasKey(pl => new {pl.PostId, pl.UserId});

        modelBuilder.Entity<PostLike>()
            .HasOne<Post>()
            .WithMany(p => p.Likes)
            .HasForeignKey(p => p.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PostLike>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void CommentLikeModelConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommentLike>()
            .HasKey(cl => new {cl.CommentId, cl.UserId});

        modelBuilder.Entity<CommentLike>()
            .HasOne<Comment>()
            .WithMany(p => p.Likes)
            .HasForeignKey(p => p.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommentLike>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void UserModelConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
                    .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .OwnsOne(u => u.ProfilePicture, pic =>
            {
                pic.Property(p => p.Url).HasColumnName("ProfilePictureUrl");
                pic.Property(p => p.FilePath).HasColumnName("ProfilePictureFilePath");
                pic.Property(p => p.MediaType).HasColumnName("ProfilePictureMediaType");
            });

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
}

