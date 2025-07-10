using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Roles;

internal class ChatterRoleEntityTypeConfiguration : IEntityTypeConfiguration<ChatterRole>
{
    public void Configure(EntityTypeBuilder<ChatterRole> builder)
    {
        builder.ToTable("ChatterRoles");

        builder.HasKey(x => new { x.ChatterId, x.RoleId });

        builder.HasOne(x => x.Chatter)
            .WithMany()
            .HasForeignKey(x => x.ChatterId);

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
    }
}
