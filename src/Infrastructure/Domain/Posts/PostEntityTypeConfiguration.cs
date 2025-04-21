using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Domain.Posts;

namespace SocialMediaBackend.Infrastructure.Domain.Posts;

internal sealed class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(u => u.Id);

        builder.OwnsMany(p => p.MediaItems, m =>
        {
            m.WithOwner().HasForeignKey("PostId");
            m.Property<Guid>("Id");
            m.HasKey("Id");
            m.Property(p => p.Url).HasColumnName("Url");
            m.Property(p => p.FilePath).HasColumnName("FilePath");
            m.Property(p => p.MediaType).HasColumnName("MediaType");
        });;

        builder.HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
