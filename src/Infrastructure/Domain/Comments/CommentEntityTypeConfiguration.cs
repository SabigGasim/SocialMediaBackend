using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Domain.Comments;

namespace SocialMediaBackend.Infrastructure.Domain.Comments;

internal sealed class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value)
            );

        builder.Property(x => x.PostId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            );

        builder.HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
