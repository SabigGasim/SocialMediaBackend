using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Domain.Feed;
using SocialMediaBackend.Domain.Feed.Comments;

namespace SocialMediaBackend.Infrastructure.Domain.Comments;

internal sealed class CommentLikeEntityTypeConfiguration : IEntityTypeConfiguration<CommentLike>
{
    public void Configure(EntityTypeBuilder<CommentLike> builder)
    {
        builder.HasKey(cl => new { cl.CommentId, cl.UserId });

        builder.Property(x => x.CommentId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            );

        builder.Property(x => x.UserId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            );

        builder.HasOne(p => p.Comment)
            .WithMany(p => p.Likes)
            .HasForeignKey(p => p.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Author>()
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
