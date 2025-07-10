using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Roles;

internal class AuthorRoleEntityTypeConfiguration : IEntityTypeConfiguration<AuthorRole>
{
    public void Configure(EntityTypeBuilder<AuthorRole> builder)
    {
        builder.ToTable("AuthorRoles");

        builder.HasKey(x => new { x.AuthorId, x.RoleId });

        builder.HasOne(x => x.Author)
            .WithMany()
            .HasForeignKey(x => x.AuthorId);

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
    }
}
