using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.Users.Domain.Feed;
using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Domain.Authors;

internal class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value));

        builder.OwnsOne(u => u.ProfilePicture, pic =>
        {
            pic.Property(p => p.Url).HasColumnName("ProfilePictureUrl");
            pic.Property(p => p.FilePath).HasColumnName("ProfilePictureFilePath");
            pic.Property(p => p.MediaType).HasColumnName("ProfilePictureMediaType");
        });

        builder.Property(u => u.ProfileIsPublic)
            .HasDefaultValue(true);

        builder.HasIndex(u => u.Username)
            .IsUnique();
    }
}
