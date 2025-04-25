using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Infrastructure.Domain.Comments;

internal sealed class CommentLikeEntityTypeConfiguration : IEntityTypeConfiguration<CommentLike>
{
    public void Configure(EntityTypeBuilder<CommentLike> builder)
    {
        builder.HasKey(cl => new { cl.CommentId, cl.UserId });

        builder.Property(x => x.CommentId)
            .HasConversion(
                id => id.Value,
                value => new CommentId(value)
            );

        builder.Property(x => x.UserId)
            .HasConversion(
                id => id.Value,
                value => new UserId(value)
            );

        builder.HasOne(p => p.Comment)
            .WithMany(p => p.Likes)
            .HasForeignKey(p => p.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
