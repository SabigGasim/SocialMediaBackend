using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Domain.Feed;
using SocialMediaBackend.Domain.Feed.Posts;

namespace SocialMediaBackend.Infrastructure.Domain.Posts;

internal sealed class PostLikeEntityTypeConfiguration : IEntityTypeConfiguration<PostLike>
{
    public void Configure(EntityTypeBuilder<PostLike> builder)
    {
        builder.HasKey(pl => new { pl.PostId, pl.UserId });

        builder.Property(p => p.UserId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            );

        builder.Property(p => p.PostId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            );

        builder.HasOne(p => p.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(p => p.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Author>()
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
