using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Domain.Feed.Posts;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Infrastructure.Domain.Posts;

internal sealed class PostLikeEntityTypeConfiguration : IEntityTypeConfiguration<PostLike>
{
    public void Configure(EntityTypeBuilder<PostLike> builder)
    {
        builder.HasKey(pl => new { pl.PostId, pl.UserId });

        builder.Property(p => p.UserId)
            .HasConversion(
                id => id.Value,
                value => new UserId(value)
            );

        builder.Property(p => p.PostId)
            .HasConversion(
                id => id.Value,
                value => new PostId(value)
            );

        builder.HasOne(p => p.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(p => p.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
