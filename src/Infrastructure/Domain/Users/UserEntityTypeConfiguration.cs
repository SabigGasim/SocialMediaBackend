using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Infrastructure.Domain.Users;
internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new UserId(value));

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
