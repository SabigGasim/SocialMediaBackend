using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.Users.Domain.Authorization;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Domain.Permissions;

internal class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.Name)
            .IsUnique();

        builder.Property(p => p.Name)
            .IsRequired();

        var permissionsToSeed = RolePermissionData
            .Mappings
            .SelectMany(mapping => mapping.Value)
            .Distinct();

        builder.HasData(permissionsToSeed);
    }
}
