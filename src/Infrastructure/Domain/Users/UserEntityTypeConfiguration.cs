using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Domain.Users;
using System.Reflection.Emit;

namespace SocialMediaBackend.Infrastructure.Domain.Users;
internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.OwnsOne(u => u.ProfilePicture,
            pic =>
            {
                pic.Property(p => p.Url).HasColumnName("ProfilePictureUrl");
                pic.Property(p => p.FilePath).HasColumnName("ProfilePictureFilePath");
                pic.Property(p => p.MediaType).HasColumnName("ProfilePictureMediaType");
            });

        builder.HasIndex(u => u.Username)
            .IsUnique();
    }
}
